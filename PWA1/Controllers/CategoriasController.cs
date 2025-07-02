using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA1.Models;

namespace PWA1.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly PwaContext _context;

        public CategoriasController(PwaContext context)
        {
            _context = context;
        }

        public IActionResult Hogar()
        {
            var resenias = _context.Reseñas
                .Include(r => r.Usuario)
                .Include(r => r.Categoria)
                .Where(r => r.Categoria.Nombre == "Hogar")
                .OrderByDescending(r => r.FechaReseña)
                .ToList();

            return View("CategoriaResenias", resenias);
        }

        public IActionResult ParaVisitar()
        {
            var resenias = _context.Reseñas
                .Include(r => r.Usuario)
                .Include(r => r.Categoria)
                .Where(r => r.Categoria.Nombre == "Para Visitar")
                .OrderByDescending(r => r.FechaReseña)
                .ToList();

            return View("CategoriaResenias", resenias);
        }

        public IActionResult Restaurante()
        {
            var resenias = _context.Reseñas
                .Include(r => r.Usuario)
                .Include(r => r.Categoria)
                .Where(r => r.Categoria.Nombre == "Restaurantes")
                .OrderByDescending(r => r.FechaReseña)
                .ToList();

            return View("CategoriaResenias", resenias);
        }

        public IActionResult Tecnologia()
        {
            var resenias = _context.Reseñas
                .Include(r => r.Usuario)
                .Include(r => r.Categoria)
                .Where(r => r.Categoria.Nombre == "Tecnología")
                .OrderByDescending(r => r.FechaReseña)
                .ToList();

            return View("CategoriaResenias", resenias);
        }
    }
}

