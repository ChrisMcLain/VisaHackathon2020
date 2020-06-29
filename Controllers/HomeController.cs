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

        public IActionResult Search(float lat, float lng, string category, bool expanded = false)
        {
            if (string.IsNullOrEmpty(category))
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
                Category = category
            };

            var response = MerchantLocatorService.GetMerchantsNear(request);
            
            var model = new SearchModel
            {
                Latitude = lat,
                Longitude = lng,
                Response = response,
                ExpandedSearch = expanded
            };
            
            return View(model);
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