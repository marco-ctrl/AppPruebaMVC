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
    public class TriajesController : Controller
    {
        private readonly consultoriobdContext _context;

        public TriajesController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Triajes
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Triajes.Include(t => t.CodigoCitaNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Triajes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Triajes == null)
            {
                return NotFound();
            }

            var triaje = await _context.Triajes
                .Include(t => t.CodigoCitaNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (triaje == null)
            {
                return NotFound();
            }

            return View(triaje);
        }

        // GET: Triajes/Create
        public IActionResult Create()
        {
            ViewData["CodigoCita"] = new SelectList(_context.Cita, "Codigo", "Codigo");
            return View();
        }

        // POST: Triajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FrecuenciaCardiaca,FrecuenciaRespiratoria,Ocupacion,Peso,PresionArterial,Saturacion,Temperatura,Codigo,CodigoCita")] Triaje triaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(triaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCita"] = new SelectList(_context.Cita, "Codigo", "Codigo", triaje.CodigoCita);
            return View(triaje);
        }

        // GET: Triajes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Triajes == null)
            {
                return NotFound();
            }

            var triaje = await _context.Triajes.FindAsync(id);
            if (triaje == null)
            {
                return NotFound();
            }
            ViewData["CodigoCita"] = new SelectList(_context.Cita, "Codigo", "Codigo", triaje.CodigoCita);
            return View(triaje);
        }

        // POST: Triajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FrecuenciaCardiaca,FrecuenciaRespiratoria,Ocupacion,Peso,PresionArterial,Saturacion,Temperatura,Codigo,CodigoCita")] Triaje triaje)
        {
            if (id != triaje.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(triaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TriajeExists(triaje.Codigo))
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
            ViewData["CodigoCita"] = new SelectList(_context.Cita, "Codigo", "Codigo", triaje.CodigoCita);
            return View(triaje);
        }

        // GET: Triajes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Triajes == null)
            {
                return NotFound();
            }

            var triaje = await _context.Triajes
                .Include(t => t.CodigoCitaNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (triaje == null)
            {
                return NotFound();
            }

            return View(triaje);
        }

        // POST: Triajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Triajes == null)
            {
                return Problem("Entity set 'consultoriobdContext.Triajes'  is null.");
            }
            var triaje = await _context.Triajes.FindAsync(id);
            if (triaje != null)
            {
                _context.Triajes.Remove(triaje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TriajeExists(string id)
        {
          return (_context.Triajes?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
