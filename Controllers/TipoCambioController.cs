using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using apptienda.Integration.Exchange;
using apptienda.Dto;

namespace apptienda.Controllers
{
    public class TipoCambioController : Controller
    {
        private readonly ILogger<TipoCambioController> _logger;
        private readonly ExchangeIntegration _exchange;

        public TipoCambioController(ILogger<TipoCambioController> logger,
        ExchangeIntegration exchange)
        {
            _logger = logger;
            _exchange = exchange;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Exchange(TipoCambio? tipoCambio)
        {
            double rate = await _exchange.GetExchangeRate(tipoCambio);
            var cambio = tipoCambio.Cantidad * rate;
            _logger.LogInformation($"Tipo de cambio de {tipoCambio.From} a {tipoCambio.To} es {rate} y cambio {cambio}");
            ViewData["rate"] = String.Format("{0:F2}", rate);
            ViewData["cambio"] = String.Format("{0:N2}", cambio); ;
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}