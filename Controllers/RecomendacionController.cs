using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using apptienda.Data;
using apptienda.ML;
using System.Security.Claims;
using apptienda.Models;
using Microsoft.ML;


namespace apptienda.Controllers
{
    public class RecomendacionController : Controller
    {
        private readonly ILogger<RecomendacionController> _logger;
        private readonly ApplicationDbContext _context;

        public RecomendacionController(ILogger<RecomendacionController> logger,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine("User ID: " + userId);
            List<(int productId, float normalizedScore)> ratings = new List<(int productId, float normalizedScore)>();
            var userRatings = _context.DbSetRating
                .Where(r => r.UserId == userId)
                .Select(r => new { r.Product.Id, r.RatingValue })
                .ToList();
            List<Producto> ProductosComprados = new List<Producto>();

            foreach (var rating in userRatings)
            {
                ProductosComprados.Add(_context.DbSetProducto.Find(rating.Id));
            }


            foreach (var producto in GetProductRecomendados())
            {
                var sampleData = new MLModelRecomendation.ModelInput()
                {
                    UserId = userId,
                    ProductId = producto.Id,
                };
                //Load model and predict output
                var prediction = MLModelRecomendation.Predict(sampleData);
                Console.WriteLine($"Producto: {producto.Name}, Score: {prediction.Score}");
                float normalizedscore = float.IsNaN(prediction.Score) ? 0f : Sigmoid(prediction.Score);
                Console.WriteLine($"Normalized Score: {normalizedscore}");
                ratings.Add((producto.Id, normalizedscore));
            }

            ViewData["productosComprados"] = ProductosComprados;
            ViewData["ratings"] = ratings;
            ViewData["GetProductRecomendados"] = GetProductRecomendados();

            return View();
        }

        [HttpPost]
        public IActionResult Retrain()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            // 1. Leer ratings reales de la BD
            var dbRatings = _context.DbSetRating
                .Include(r => r.Product)
                .Where(r => r.UserId != null && r.Product != null)
                .Select(r => new { r.UserId, ProductId = r.Product.Id, r.RatingValue })
                .ToList();

            // 2. Ruta del CSV de entrenamiento
            string csvPath = Path.GetFullPath("./ML/RecomendationMatrix/trainningdata/product_ratings_simple_10k.csv");

            // 3. Leer filas existentes para evitar duplicados
            var existingLines = new HashSet<string>(System.IO.File.ReadAllLines(csvPath).Skip(1).Select(l => l.Trim()));

            // 4. Agregar solo los ratings nuevos
            var newLines = new List<string>();
            foreach (var r in dbRatings)
            {
                var line = $"{r.UserId},{r.ProductId},{r.RatingValue}";
                if (existingLines.Add(line))
                    newLines.Add(line);
            }

            if (newLines.Count > 0)
                System.IO.File.AppendAllLines(csvPath, newLines);

            // 5. Reentrenar el modelo con el CSV actualizado
            string modelPath = Path.GetFullPath("./ML/RecomendationMatrix/MLModelRecomendation.mlnet");
            MLModelRecomendation.Train(modelPath, csvPath, ',', true);

            // 6. Recargar el motor de predicción
            MLModelRecomendation.ReloadModel();

            int totalLines = System.IO.File.ReadAllLines(csvPath).Length - 1; // excluir header
            _logger.LogInformation("Modelo reentrenado. Nuevos registros: {New}. Total en CSV: {Total}", newLines.Count, totalLines);
            TempData["RetrainMessage"] = $"Modelo reentrenado. Se agregaron {newLines.Count} nuevos registros. Total en dataset: {totalLines} filas.";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Diagnóstico: muestra el estado actual del modelo, datos en BD y predicciones en bruto.
        /// Acceder a /Recomendacion/Diagnostico
        /// </summary>
        public IActionResult Diagnostico()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string csvPath = Path.GetFullPath("./ML/RecomendationMatrix/trainningdata/product_ratings_simple_10k.csv");

            // Ratings en la BD para este usuario
            var dbRatings = _context.DbSetRating
                .Include(r => r.Product)
                .Where(r => r.UserId == userId)
                .Select(r => new { r.Product.Id, r.Product.Name, r.RatingValue })
                .ToList();

            // Conteo del CSV
            var csvLines = System.IO.File.ReadAllLines(csvPath);
            int totalCsvRows = csvLines.Length - 1;
            bool userInCsv = csvLines.Skip(1).Any(l => l.StartsWith(userId));

            // Predicciones brutas para productos recomendados
            var productos = GetProductRecomendados();
            var predictions = productos.Select(p =>
            {
                var input = new MLModelRecomendation.ModelInput { UserId = userId, ProductId = p.Id };
                var output = MLModelRecomendation.Predict(input);
                float sigmoid = float.IsNaN(output.Score) ? 0f : Sigmoid(output.Score);
                return new
                {
                    p.Id,
                    p.Name,
                    RawScore = output.Score,
                    IsNaN = float.IsNaN(output.Score),
                    NormalizedScore = sigmoid
                };
            }).ToList();

            return Json(new
            {
                userId,
                userInCsv,
                totalCsvRows,
                dbRatingsCount = dbRatings.Count,
                dbRatings,
                recommendedProductsCount = productos.Count,
                predictions
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        private List<Producto> GetProductRecomendados()
        {
            List<Producto> productos = new List<Producto>();
            productos = _context.DbSetProducto
                .Where(p => p.IsRecommended == true)
                .ToList();
            return productos;
        }

        public float Sigmoid(float x)
        {
            return (float)(100 / (1 + Math.Exp(-x)));
        }
    }
}