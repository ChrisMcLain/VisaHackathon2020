using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VisaHackathon2020.Giveback;
using VisaHackathon2020.Models;

namespace VisaHackathon2020.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Search(float lat, float lng, int[] category, bool expanded = false)
        {
            if (category.Length == 0)
            {
                return View(new SearchModel
                {
                    ExpandedSearch = expanded,
                });
            }
            
            var request = new MerchantLocatorRequest
            {
                Latitude = lat,
                Longitude = lng,
                Category = category,
                Distance = 99,
                StartIndex = 0
            };

            var response = MerchantLocatorService.GetMerchantsNear(request, 2);
            
            var model = new SearchModel
            {
                Latitude = lat,
                Longitude = lng,
                Response = response,
                Category = category,
                ExpandedSearch = expanded
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Donate(long merchant, long amount, string cardHolder, string cardNumber, 
            string cardExpiry, string cardCvc, string message)
        {
            var request = new FundsTransferRequest
            {
                MerchantId = merchant,
                Amount = amount,
                CardNumber = cardNumber,
                CardExpiryDate = cardExpiry,
                LocalTransactionDateTime = DateTimeOffset.Now
            };
            
            return RedirectToAction("Success");
        }
        
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}