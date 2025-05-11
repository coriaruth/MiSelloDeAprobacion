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
            var resenias = ObtenerReseñas().Take(4).ToList();

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
        public IActionResult Reseñas()
        {
            return View();
        }


        private List<Reseña> ObtenerReseñas()
        {
            return new List<Reseña>
            {
                new Reseña
            {
                    Id = 1,
                    titulo = "Los mejores auriculares",
                    imagen = "/imag/auriculares.png",
                    detalle="Son considerados por muchos como los mejores en cancelación de ruido actualmente, además de ofrecer un sonido excelente y un diseño cómodo.",
                    usuario="RCORIA"

            },
                  new Reseña
            {
                    Id = 2,
                    titulo = "Los mejores laptop",
                    imagen = "/imag/Laptop.png",
                    detalle="Son considerados por muchos como los mejores en cancelación de ruido actualmente, además de ofrecer un sonido excelente y un diseño cómodo.",
                    usuario="RCORIA"

            }, new Reseña
            {
                    Id = 3,
                    titulo = "Los mejores net",
                    imagen = "/imag/net.png",
                    detalle="Son considerados por muchos como los mejores en cancelación de ruido actualmente, además de ofrecer un sonido excelente y un diseño cómodo.",
                    usuario="RCORIA"

            },  new Reseña
            {
                    Id = 4,
                    titulo = "Los mejores smatwatch",
                    imagen = "/imag/smartwatch.png",
                    detalle="Son considerados por muchos como los mejores en cancelación de ruido actualmente, además de ofrecer un sonido excelente y un diseño cómodo.",
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
