using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using apptienda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using apptienda.Data;

namespace apptienda.Controllers
{

    public class RatingController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ILogger<RatingController> logger,
            ApplicationDbContext context)
        {
            _context = context;

            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RatingInputModel input)
        {

            _logger.LogInformation("Rating: ProductId={ProductId}, RatingValue={RatingValue}", input.ProductId, input.RatingValue);
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation("UserId: {0}", userId);
            // Opcional: evitar que el usuario califique el mismo producto más de una vez
            var existing = _context.DbSetRating.FirstOrDefault(r => r.Product.Id == input.ProductId && r.UserId == userId);
            _logger.LogInformation("Existing: {0}", existing);
            if (existing != null)
            {
                existing.RatingValue = input.RatingValue;
            }
            else
            {

                var product = _context.DbSetProducto.FirstOrDefault(p => p.Id == input.ProductId);
                var rating = new Rating
                {
                    Product = product,
                    UserId = userId,
                    RatingValue = input.RatingValue
                };
                _logger.LogInformation("Rating: {0}", rating);
                _context.DbSetRating.Add(rating);
            }
            _logger.LogInformation("SaveChanges");
            _context.SaveChanges();
            return Ok();
        }

        public class RatingInputModel
        {
            public int ProductId { get; set; }
            public int RatingValue { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}