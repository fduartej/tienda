using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using apptienda.Models;
using apptienda.Data;
using apptienda.Helpers;

namespace apptienda.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CustomerController(ILogger<CustomerController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var userName = _userManager.GetUserName(User);
            Customer customer = new Customer();
            customer.UserName = userName;
            return View(customer);
        }

        public IActionResult RegistrarInfo(Customer customer)
        {
            _logger.LogInformation("RegistrarInfo {1}", customer);
            if (ModelState.IsValid)
            {
                _logger.LogInformation("RegistrarInfo {1}", customer);
                // Convert BirthDate to UTC
                if (customer.BirthDate.Kind == DateTimeKind.Unspecified)
                {
                    customer.BirthDate = DateTime.SpecifyKind(customer.BirthDate, DateTimeKind.Utc);
                }
                else if (customer.BirthDate.Kind == DateTimeKind.Local)
                {
                    customer.BirthDate = customer.BirthDate.ToUniversalTime();
                }

                SessionExtension.Set<Customer>(HttpContext.Session, "CustomerSession", customer);

                _context.DbSetCustomer.Add(customer);
                _context.SaveChanges();
                ViewData["Message"] = "Se registró los datos del cliente";
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