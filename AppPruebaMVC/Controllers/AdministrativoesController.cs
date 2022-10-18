using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;

namespace AppPruebaMVC.Controllers
{
    public class AdministrativoesController : Controller
    {
        private readonly consultoriobdContext _context;

        public AdministrativoesController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Administrativoes
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Administrativos.Include(a => a.CodigoNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Administrativoes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Administrativos == null)
            {
                return NotFound();
            }

            var administrativo = await _context.Administrativos
                .Include(a => a.CodigoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (administrativo == null)
            {
                return NotFound();
            }

            return View(administrativo);
        }

        // GET: Administrativoes/Create
        public IActionResult Create()
        {
            ViewData["Codigo"] = new SelectList(_context.Personas, "Codigo", "Codigo");
            return View();
        }

        // POST: Administrativoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cargo,Codigo")] Administrativo administrativo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrativo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Codigo"] = new SelectList(_context.Personas, "Codigo", "Codigo", administrativo.Codigo);
            return View(administrativo);
        }

        // GET: Administrativoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Administrativos == null)
            {
                return NotFound();
            }

            var administrativo = await _context.Administrativos.FindAsync(id);
            if (administrativo == null)
            {
                return NotFound();
            }
            ViewData["Codigo"] = new SelectList(_context.Personas, "Codigo", "Codigo", administrativo.Codigo);
            return View(administrativo);
        }

        // POST: Administrativoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cargo,Codigo")] Administrativo administrativo)
        {
            if (id != administrativo.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrativo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrativoExists(administrativo.Codigo))
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
            ViewData["Codigo"] = new SelectList(_context.Personas, "Codigo", "Codigo", administrativo.Codigo);
            return View(administrativo);
        }

        // GET: Administrativoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Administrativos == null)
            {
                return NotFound();
            }

            var administrativo = await _context.Administrativos
                .Include(a => a.CodigoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (administrativo == null)
            {
                return NotFound();
            }

            return View(administrativo);
        }

        // POST: Administrativoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Administrativos == null)
            {
                return Problem("Entity set 'consultoriobdContext.Administrativos'  is null.");
            }
            var administrativo = await _context.Administrativos.FindAsync(id);
            if (administrativo != null)
            {
                _context.Administrativos.Remove(administrativo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrativoExists(int id)
        {
          return _context.Administrativos.Any(e => e.Codigo == id);
        }
    }
}
