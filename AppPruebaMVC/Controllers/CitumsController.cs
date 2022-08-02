using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC.Controllers
{
    public class CitumsController : Controller
    {
        private readonly consultoriobdContext _context;

        public CitumsController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Citums
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Cita.Include(c => c.PacienteNavigation).Include(c => c.UsuarioNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Citums/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita
                .Include(c => c.PacienteNavigation)
                .Include(c => c.UsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (citum == null)
            {
                return NotFound();
            }

            return View(citum);
        }

        // GET: Citums/Create
        public IActionResult Create()
        {
            ViewData["Paciente"] = new SelectList(_context.Personas, "Codigo", "Codigo");
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "Codigo", "Codigo");
            return View();
        }

        // POST: Citums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Estado,Fecha,Hora,Usuario,Codigo,Paciente")] Citum citum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Paciente"] = new SelectList(_context.Personas, "Codigo", "Codigo", citum.Paciente);
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", citum.Usuario);
            return View(citum);
        }

        // GET: Citums/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita.FindAsync(id);
            if (citum == null)
            {
                return NotFound();
            }
            ViewData["Paciente"] = new SelectList(_context.Personas, "Codigo", "Codigo", citum.Paciente);
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", citum.Usuario);
            return View(citum);
        }

        // POST: Citums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Estado,Fecha,Hora,Usuario,Codigo,Paciente")] Citum citum)
        {
            if (id != citum.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitumExists(citum.Codigo))
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
            ViewData["Paciente"] = new SelectList(_context.Personas, "Codigo", "Codigo", citum.Paciente);
            ViewData["Usuario"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", citum.Usuario);
            return View(citum);
        }

        // GET: Citums/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Cita == null)
            {
                return NotFound();
            }

            var citum = await _context.Cita
                .Include(c => c.PacienteNavigation)
                .Include(c => c.UsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (citum == null)
            {
                return NotFound();
            }

            return View(citum);
        }

        // POST: Citums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Cita == null)
            {
                return Problem("Entity set 'consultoriobdContext.Cita'  is null.");
            }
            var citum = await _context.Cita.FindAsync(id);
            if (citum != null)
            {
                _context.Cita.Remove(citum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitumExists(string id)
        {
            return (_context.Cita?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
