using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly consultoriobdContext _context;

        public UsuariosController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Usuarios.Include(u => u.CodPersonaNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.CodPersonaNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["CodPersona"] = new SelectList(_context.Personas, "Codigo", "Nombre", "");
            ViewData["Nombre"] = new SelectList(_context.Personas, "Codigo", "Codigo");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Contrasena,Correo,Usuario1,Codigo,CodPersona")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Estado = true;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodPersona"] = new SelectList(_context.Personas, "Codigo", "Codigo", usuario.CodPersona);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["CodPersona"] = new SelectList(_context.Personas, "Codigo", "Codigo", usuario.CodPersona);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Contrasena,Correo,Usuario1,Codigo,CodPersona")] Usuario usuario)
        {
            if (id != usuario.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Estado = true;
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodPersona"] = new SelectList(_context.Personas, "Codigo", "Codigo", usuario.CodPersona);
            return View(usuario);
        }

        public async Task<IActionResult> Baja(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["CodPersona"] = new SelectList(_context.Personas, "Codigo", "Codigo", usuario.CodPersona);
            return PartialView("Baja", usuario);
        }

        [HttpPost, ActionName("DarBaja")]
        public async Task<IActionResult> ConfirmarBaja(int id, [Bind("Contrasena,Correo,Usuario1,Codigo,CodPersona,Estado")] Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //usuario.Estado = true;
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            //*ViewData["CodPersona"] = new SelectList(_context.Personas, "Codigo", "Codigo", usuario.CodPersona);
            return Json(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.CodPersonaNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'consultoriobdContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int? id)
        {
            return _context.Usuarios.Any(e => e.Codigo == id);
        }
    }
}
