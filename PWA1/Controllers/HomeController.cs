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
            Usuario usuario = new Usuario();
            var resenias = ObtenerRese�as().Take(4).ToList();

            var modelo = new PorfolioVM()
            {
                usuario = usuario,
                resenias = resenias

            };


            return View(modelo);

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
        public IActionResult Rese�as()
        {
            return View();
        }


        private List<Rese�a> ObtenerRese�as()
        {
            return new List<Rese�a>
            {
                new Rese�a
            {
                    Id = 1,
                    titulo = "Los mejores auriculares",
                    imagen = "/imag/auriculares.png",
                    detalle="Son considerados por muchos como los mejores en cancelaci�n de ruido actualmente, adem�s de ofrecer un sonido excelente y un dise�o c�modo.",
                    usuario="RCORIA"

            },
                  new Rese�a
            {
                    Id = 2,
                    titulo = "Los mejores laptop",
                    imagen = "/imag/Laptop.png",
                    detalle="Son considerados por muchos como los mejores en cancelaci�n de ruido actualmente, adem�s de ofrecer un sonido excelente y un dise�o c�modo.",
                    usuario="RCORIA"

            }, new Rese�a
            {
                    Id = 3,
                    titulo = "Los mejores net",
                    imagen = "/imag/net.png",
                    detalle="Son considerados por muchos como los mejores en cancelaci�n de ruido actualmente, adem�s de ofrecer un sonido excelente y un dise�o c�modo.",
                    usuario="RCORIA"

            },  new Rese�a
            {
                    Id = 4,
                    titulo = "Los mejores smatwatch",
                    imagen = "/imag/smartwatch.png",
                    detalle="Son considerados por muchos como los mejores en cancelaci�n de ruido actualmente, adem�s de ofrecer un sonido excelente y un dise�o c�modo.",
                    usuario="RCORIA"

            }


            };
            
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
