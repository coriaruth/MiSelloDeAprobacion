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
            var reseñas = ObtenerReseñasPorCategoria("Hogar");
            return View(reseñas);
        }

        public IActionResult Tecnologia()
        {
            var reseñas = ObtenerReseñasPorCategoria("Tecnologia");
            return View(reseñas);
        }

        public IActionResult Restaurantes()
        {
            var reseñas = ObtenerReseñasPorCategoria("Restaurantes");
            return View(reseñas);
        }

        public IActionResult ParaVisitar()
        {
            var reseñas = ObtenerReseñasPorCategoria("Para Visitar");
            return View(reseñas);
        }

        // Método privado para traer reseñas filtradas
        private List<Reseña> ObtenerReseñasPorCategoria(string nombreCategoria)
        {
            return _context.Reseñas
                .Include(r => r.Usuario)
                .Include(r => r.Categoria)
                .Include(r => r.Subcategoria)
                .Where(r => r.Categoria.Nombre == nombreCategoria)
                .OrderByDescending(r => r.FechaReseña)
                .ToList();
        }
    }
}


