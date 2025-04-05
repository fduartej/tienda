using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using apptienda.Data;
using apptienda.Models;

namespace apptienda.Controllers
{

    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly ApplicationDbContext _context;
        public PedidoController(ILogger<PedidoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var pedidos = from o in _context.DbSetOrden select o;
            pedidos = pedidos.Include(p => p.Pago).Where(s => s.Status.Contains("PENDIENTE"));
            return View(await pedidos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var itemsPedido = from o in _context.DbSetDetalleOrden select o;
            itemsPedido = itemsPedido.
                Include(p => p.Producto).
                Where(s => s.Id.Equals(id));
            return View(await itemsPedido.ToListAsync());

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}