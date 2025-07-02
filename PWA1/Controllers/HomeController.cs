using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA1.Models;
using System.Diagnostics;

namespace PWA1.Controllers
{
    public class HomeController : Controller
    {
        private readonly PwaContext _DbContext;

        public HomeController(PwaContext context)
        {
            _DbContext = context;
        }

        public IActionResult Index()
        {
            Usuario usuario = new Usuario();

            var resenias = ObtenerReseñas()
                .Take(4)
                .ToList();

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

        [HttpPost]
        public IActionResult Registrar(string nombre, string email, string contraseña, string pais, string sexo)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contraseña))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View();
            }

            var existe = _DbContext.Usuarios.Any(u => u.Nombre == nombre);
            if (existe)
            {
                ViewBag.Error = "El usuario ya existe.";
                return View();
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                Contraseña = contraseña,
                FechaRegistro = DateTime.Now,
                Pais = pais,
                Sexo = sexo
            };

            _DbContext.Usuarios.Add(nuevoUsuario);
            _DbContext.SaveChanges();

            ViewBag.RegistroExitoso = true;
            return RedirectToAction("MiCuenta");
        }

        public IActionResult MiCuenta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MiCuenta(string usuario, string contraseña)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View();
            }

            var user = _DbContext.Usuarios
                .FirstOrDefault(u => u.Nombre == usuario && u.Contraseña == contraseña);

            if (user != null)
            {
                // Guarda en sesión el nombre
                HttpContext.Session.SetString("Usuario", user.Nombre);

                // Guarda también el ID del usuario (ESTO ES CLAVE para ReseniasController)
                HttpContext.Session.SetInt32("UsuarioId", user.UsuarioId);

                // Redirige a la acción Usuario
                return RedirectToAction("Usuario");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }
        }

        public IActionResult Usuario()
        {
            var nombreUsuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                // Si no hay usuario logueado, vuelve a MiCuenta para login
                return RedirectToAction("MiCuenta");
            }

            // Busca el usuario en base
            var usuario = _DbContext.Usuarios
                .FirstOrDefault(u => u.Nombre == nombreUsuario);

            if (usuario == null)
            {
                // Usuario no encontrado
                return RedirectToAction("MiCuenta");
            }

            // Cargar reseñas del usuario
            var reseñasUsuario = _DbContext.Reseñas
                .Include(r => r.Categoria)
                .Include(r => r.Subcategoria)
                .Where(r => r.UsuarioId == usuario.UsuarioId)
                .OrderByDescending(r => r.FechaReseña)
                .ToList();

            ViewBag.Usuario = usuario.Nombre;

            return View(reseñasUsuario);
        }

        private List<Reseña> ObtenerReseñas()
        {
            return _DbContext.Reseñas
                .Include(r => r.Usuario)
                .Include(r => r.Categoria)
                .Include(r => r.Subcategoria)
                .OrderByDescending(r => r.FechaReseña)
                .ToList();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




