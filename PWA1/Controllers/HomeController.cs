using Microsoft.AspNetCore.Mvc;
using PWA1.Models;
using System.Diagnostics;

namespace PWA1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult MiCuenta()
        {
            return View();
        }
        public IActionResult SobreNosotros()
        {
            return View();
        }
        public IActionResult Reseñas()
        {
            return View();
        }

       


        public IActionResult Registrar()
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
