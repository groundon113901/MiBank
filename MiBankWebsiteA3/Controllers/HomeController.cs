using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiBankWebsiteA2.Models;
using MiBankWebsiteA2.Data;
using MiBankWebsiteA2.Attributes;

namespace MiBankWebsiteA2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MiBankContext _context;

        public HomeController(ILogger<HomeController> logger, MiBankContext context)
        {
            _context = context;
            _logger = logger;
        }

        [ServiceFilter(typeof(RequiresLogin))]
        public IActionResult Index()
        {
           return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
