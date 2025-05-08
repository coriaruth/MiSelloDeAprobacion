using Microsoft.AspNetCore.Mvc;

namespace PWA1.Controllers
{
    public class CategoriasController : Controller
    {
        public IActionResult Hogar()
        {
            return View("Hogar","Para el Hogar");
        }

        public IActionResult ParaVisitar()
        {
            return View("ParaVisitar", "Para Visitar");
        }
        public IActionResult Restaurante()
        {
            return View("Restaurantes", "Para Conocer");
        }
        public IActionResult Tecnologia()
        {
            return View("Tecnologia", "Para Cambiar");
        }
    }
}
