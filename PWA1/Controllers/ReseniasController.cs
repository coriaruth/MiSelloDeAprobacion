using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PWA1.Models;

namespace PWA1.Controllers
{
    public class ReseniasController : Controller
    {
        private readonly PwaContext _context;
        private readonly IWebHostEnvironment _env;

        public ReseniasController(PwaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: /Resenias/Reseñas (Crear)
        public IActionResult Reseñas()
        {
            CargarCombos();
            return View();
        }

        // POST: /Resenias/Reseñas
        [HttpPost]
        public IActionResult Reseñas(Reseña reseña, IFormFile? imagen)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return Unauthorized();

            if (imagen != null)
            {
                var nombreArchivo = Guid.NewGuid() + Path.GetExtension(imagen.FileName);
                var carpeta = Path.Combine(_env.WebRootPath, "imag");
                var ruta = Path.Combine(carpeta, nombreArchivo);

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                using (var stream = System.IO.File.Create(ruta))
                    imagen.CopyTo(stream);

                reseña.ImagenRuta = "/imag/" + nombreArchivo;
            }

            reseña.UsuarioId = usuarioId.Value;
            reseña.FechaReseña = DateTime.Now;

            _context.Reseñas.Add(reseña);
            _context.SaveChanges();

            return RedirectToAction("Usuario", "Home");
        }

        // GET: /Resenias/EditarR/5
        public IActionResult EditarR(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return Unauthorized();

            var reseña = _context.Reseñas
                .Include(r => r.Categoria)
                .Include(r => r.Subcategoria)
                .FirstOrDefault(r => r.ReseñaId == id && r.UsuarioId == usuarioId.Value);

            if (reseña == null)
                return NotFound();

            CargarCombos();
            return View(reseña);
        }

        // POST: /Resenias/EditarR
        [HttpPost]
        public IActionResult EditarR(Reseña reseña, IFormFile? imagen)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return Unauthorized();

            var reseñaExistente = _context.Reseñas.AsNoTracking()
                .FirstOrDefault(r => r.ReseñaId == reseña.ReseñaId && r.UsuarioId == usuarioId.Value);

            if (reseñaExistente == null)
                return NotFound();

            reseña.ImagenRuta = reseñaExistente.ImagenRuta;

            if (imagen != null)
            {
                var nombreArchivo = Guid.NewGuid() + Path.GetExtension(imagen.FileName);
                var carpeta = Path.Combine(_env.WebRootPath, "imag");
                var ruta = Path.Combine(carpeta, nombreArchivo);

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                using (var stream = System.IO.File.Create(ruta))
                    imagen.CopyTo(stream);

                reseña.ImagenRuta = "/imag/" + nombreArchivo;
            }

            reseña.UsuarioId = usuarioId.Value;
            reseña.FechaReseña = DateTime.Now;

            _context.Reseñas.Update(reseña);
            _context.SaveChanges();

            return RedirectToAction("Usuario", "Home");
        }

        // POST: /Resenias/BorrarConfirmado/5
        [HttpPost]
        public IActionResult BorrarConfirmado(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return Unauthorized();

            var reseña = _context.Reseñas
                .FirstOrDefault(r => r.ReseñaId == id && r.UsuarioId == usuarioId.Value);

            if (reseña == null)
                return NotFound();

            _context.Reseñas.Remove(reseña);
            _context.SaveChanges();

            return RedirectToAction("Usuario", "Home");
        }

        private void CargarCombos()
        {
            ViewBag.Categorias = _context.Categoria
                .Select(c => new SelectListItem
                {
                    Value = c.CategoriaId.ToString(),
                    Text = c.Nombre
                })
                .ToList();

            ViewBag.Subcategorias = _context.Subcategoria
                .Select(s => new SelectListItem
                {
                    Value = s.SubcategoriaId.ToString(),
                    Text = s.Nombre
                })
                .ToList();
        }
    }
}











