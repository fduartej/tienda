using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using apptienda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using apptienda.Data;

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
