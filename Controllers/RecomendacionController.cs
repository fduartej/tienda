using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

                float normalizedscore = Sigmoid(prediction.Score);
                ratings.Add((producto.Id, normalizedscore));
            }

            ViewData["productosComprados"] = ProductosComprados;
            ViewData["ratings"] = ratings;
            ViewData["GetProductRecomendados"] = GetProductRecomendados();

            return View();
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