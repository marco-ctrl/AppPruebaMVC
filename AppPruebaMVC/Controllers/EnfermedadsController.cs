using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC.Controllers
{
    public class EnfermedadsController : Controller
    {
        private readonly consultoriobdContext _context;

        public EnfermedadsController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Enfermedads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enfermedads.ToListAsync());
        }

        // GET: Enfermedads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enfermedads == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedads
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (enfermedad == null)
            {
                return NotFound();
            }

            return View(enfermedad);
        }

        // GET: Enfermedads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enfermedads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,Codigo")] Enfermedad enfermedad)
        {
            if (ModelState.IsValid)
            {
                enfermedad.Estado = true;
                enfermedad.Nombre = enfermedad.Nombre.ToUpper();
                enfermedad.Descripcion = enfermedad.Descripcion.ToUpper();
                _context.Add(enfermedad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enfermedad);
        }

        // GET: Enfermedads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enfermedads == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedads.FindAsync(id);
            if (enfermedad == null)
            {
                return NotFound();
            }
            return View(enfermedad);
        }

        // POST: Enfermedads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Descripcion,Codigo")] Enfermedad enfermedad)
        {
            if (id != enfermedad.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    enfermedad.Estado = true;
                    enfermedad.Nombre = enfermedad.Nombre.ToUpper();
                    enfermedad.Descripcion = enfermedad.Descripcion.ToUpper();
                    _context.Update(enfermedad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermedadExists(enfermedad.Codigo))
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
            return View(enfermedad);
        }

        // GET: Enfermedads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enfermedads == null)
            {
                return NotFound();
            }

            var enfermedad = await _context.Enfermedads
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (enfermedad == null)
            {
                return NotFound();
            }

            return View(enfermedad);
        }

        // POST: Enfermedads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Enfermedads == null)
            {
                return Problem("Entity set 'consultoriobdContext.Enfermedads'  is null.");
            }
            var enfermedad = await _context.Enfermedads.FindAsync(id);
            if (enfermedad != null)
            {
                _context.Enfermedads.Remove(enfermedad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermedadExists(int? id)
        {
            return _context.Enfermedads.Any(e => e.Codigo == id);
        }
    }
}
