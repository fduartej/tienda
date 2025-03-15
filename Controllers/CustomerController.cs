using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace apptienda.Controllers
{
     public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerController(ILogger<CustomerController> logger,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
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
                return View("Index");
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