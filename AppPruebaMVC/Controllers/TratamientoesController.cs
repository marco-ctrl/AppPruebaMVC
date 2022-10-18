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
    public class TratamientoesController : Controller
    {
        private readonly consultoriobdContext _context;

        public TratamientoesController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Tratamientoes
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Tratamientos.Include(t => t.CodCitaNavigation).Include(t => t.CodRecetaMedicaNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Tratamientoes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Tratamientos == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamientos
                .Include(t => t.CodCitaNavigation)
                .Include(t => t.CodRecetaMedicaNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return View(tratamiento);
        }

        // GET: Tratamientoes/Create
        public IActionResult Create()
        {
            ViewData["CodCita"] = new SelectList(_context.CitaMedicas, "Codigo", "Codigo");
            ViewData["CodRecetaMedica"] = new SelectList(_context.RecetaMedicas, "Codigo", "Codigo");
            return View();
        }

        // POST: Tratamientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,CodCita,CodRecetaMedica")] Tratamiento tratamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodCita"] = new SelectList(_context.CitaMedicas, "Codigo", "Codigo", tratamiento.CodCita);
            ViewData["CodRecetaMedica"] = new SelectList(_context.RecetaMedicas, "Codigo", "Codigo", tratamiento.CodRecetaMedica);
            return View(tratamiento);
        }

        // GET: Tratamientoes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Tratamientos == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return NotFound();
            }
            ViewData["CodCita"] = new SelectList(_context.CitaMedicas, "Codigo", "Codigo", tratamiento.CodCita);
            ViewData["CodRecetaMedica"] = new SelectList(_context.RecetaMedicas, "Codigo", "Codigo", tratamiento.CodRecetaMedica);
            return View(tratamiento);
        }

        // POST: Tratamientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,CodCita,CodRecetaMedica")] Tratamiento tratamiento)
        {
            if (id != tratamiento.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamientoExists(tratamiento.Codigo))
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
            ViewData["CodCita"] = new SelectList(_context.CitaMedicas, "Codigo", "Codigo", tratamiento.CodCita);
            ViewData["CodRecetaMedica"] = new SelectList(_context.RecetaMedicas, "Codigo", "Codigo", tratamiento.CodRecetaMedica);
            return View(tratamiento);
        }

        // GET: Tratamientoes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Tratamientos == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamientos
                .Include(t => t.CodCitaNavigation)
                .Include(t => t.CodRecetaMedicaNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return View(tratamiento);
        }

        // POST: Tratamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tratamientos == null)
            {
                return Problem("Entity set 'consultoriobdContext.Tratamientos'  is null.");
            }
            var tratamiento = await _context.Tratamientos.FindAsync(id);
            if (tratamiento != null)
            {
                _context.Tratamientos.Remove(tratamiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientoExists(int id)
        {
          return _context.Tratamientos.Any(e => e.Codigo == id);
        }
    }
}
