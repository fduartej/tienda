using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using apptienda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using apptienda.Data;
using apptienda.ML;

namespace apptienda.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;


        public ContactoController(ILogger<ContactoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //Load sample data
                    var sampleData = new MLModelSentimentalAnalysis.ModelInput()
                    {
                        Comentario = contacto.Mensaje
                    };

                    //Load model and predict output
                    var result = MLModelSentimentalAnalysis.Predict(sampleData);
                    var predictedLabel = result.PredictedLabel;
                    var scorePositive = result.Score[0];
                    var scoreNegative = result.Score[1];
                    //Check if the result is positive or negative
                    if (predictedLabel == 1)
                    {
                        contacto.Etiqueta = "Positivo";
                        contacto.Puntuacion = scorePositive;
                    }
                    else
                    {
                        contacto.Etiqueta = "Negativo";
                        contacto.Puntuacion = scoreNegative;
                    }


                    _context.DbSetContactos.Add(contacto);
                    _context.SaveChanges();
                    _logger.LogInformation("Se registró el contacto");
                    ViewData["Message"] = "Se registró el contacto";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al registrar el contacto");
                    ViewData["Message"] = "Error al registrar el contacto: " + ex.Message;
                }
            }
            else
            {
                ViewData["Message"] = "Datos de entrada no válidos";
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
