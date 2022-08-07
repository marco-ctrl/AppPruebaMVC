using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly consultoriobdContext _context;

        public LoginController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Login
        /*public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Usuarios.Include(u => u.CodigoNavigation).Include(u => u.TipoUsuarioNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }*/

        // GET: Login/Create
        public IActionResult Index()
        {
            //ViewData["Codigo"] = new SelectList(_context.Personas, "Codigo", "Codigo");
            //ViewData["TipoUsuario"] = new SelectList(_context.TipoUsuarios, "Codigo", "Codigo");
            return View();
        }

        [HttpPost]
        public IActionResult Acceso(Usuario _usuario)
        {
            if (UsuarioExists(_usuario.Correo, _usuario.Contrasena))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(_usuario);
            }
        }

        private bool UsuarioExists(string correo, string contrasena)
        {
            return (_context.Usuarios?.Any(e => e.Correo == correo && e.Contrasena == contrasena)).GetValueOrDefault();
        }
    }
}
