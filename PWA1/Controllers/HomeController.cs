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

            var resenias = ObtenerRese�as()
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

        public IActionResult Rese�as()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(string nombre, string email, string contrase�a, string pais, string sexo)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contrase�a))
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
                Contrase�a = contrase�a,
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
        public IActionResult MiCuenta(string usuario, string contrase�a)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrase�a))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View();
            }

            var user = _DbContext.Usuarios
                .FirstOrDefault(u => u.Nombre == usuario && u.Contrase�a == contrase�a);

            if (user != null)
            {
                // Guarda en sesi�n el nombre
                HttpContext.Session.SetString("Usuario", user.Nombre);

                // Guarda tambi�n el ID del usuario (ESTO ES CLAVE para ReseniasController)
                HttpContext.Session.SetInt32("UsuarioId", user.UsuarioId);

                // Redirige a la acci�n Usuario
                return RedirectToAction("Usuario");
            }
            else
            {
                ViewBag.Error = "Usuario o contrase�a incorrectos.";
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

            // Cargar rese�as del usuario
            var rese�asUsuario = _DbContext.Rese�as
                .Include(r => r.Categoria)
                .Include(r => r.Subcategoria)
                .Where(r => r.UsuarioId == usuario.UsuarioId)
                .OrderByDescending(r => r.FechaRese�a)
                .ToList();

            ViewBag.Usuario = usuario.Nombre;

            return View(rese�asUsuario);
        }

        private List<Rese�a> ObtenerRese�as()
        {
            return _DbContext.Rese�as
                .Include(r => r.Usuario)
                .Include(r => r.Categoria)
                .Include(r => r.Subcategoria)
                .OrderByDescending(r => r.FechaRese�a)
                .ToList();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




