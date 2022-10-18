using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            if (UsuarioExists(_usuario.Usuario1, _usuario.Contrasena))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _usuario.Usuario1),
                    new Claim("Usuario", _usuario.Usuario1)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            //return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Salir()
        {
            //ViewData["Codigo"] = new SelectList(_context.Personas, "Codigo", "Codigo");
            //ViewData["TipoUsuario"] = new SelectList(_context.TipoUsuarios, "Codigo", "Codigo");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        private bool UsuarioExists(string correo, string contrasena)
        {
            return (_context.Usuarios?.Any(e => e.Usuario1 == correo && e.Contrasena == contrasena)).GetValueOrDefault();
        }
    }
}
