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
    public class DetalleDiagnosticoesController : Controller
    {
        private readonly consultoriobdContext _context;

        public DetalleDiagnosticoesController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: DetalleDiagnosticoes
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.DetalleDiagnosticos.Include(d => d.DiagnosticoNavigation).Include(d => d.ResultadoNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: DetalleDiagnosticoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DetalleDiagnosticos == null)
            {
                return NotFound();
            }

            var detalleDiagnostico = await _context.DetalleDiagnosticos
                .Include(d => d.DiagnosticoNavigation)
                .Include(d => d.ResultadoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (detalleDiagnostico == null)
            {
                return NotFound();
            }

            return View(detalleDiagnostico);
        }

        // GET: DetalleDiagnosticoes/Create
        public IActionResult Create()
        {
            ViewData["Diagnostico"] = new SelectList(_context.Diagnosticos, "Codigo", "Codigo");
            ViewData["Resultado"] = new SelectList(_context.Resultados, "Codigo", "Codigo");
            return View();
        }

        // POST: DetalleDiagnosticoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo,Codigo,Resultado,Diagnostico")] DetalleDiagnostico detalleDiagnostico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleDiagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Diagnostico"] = new SelectList(_context.Diagnosticos, "Codigo", "Codigo", detalleDiagnostico.Diagnostico);
            ViewData["Resultado"] = new SelectList(_context.Resultados, "Codigo", "Codigo", detalleDiagnostico.Resultado);
            return View(detalleDiagnostico);
        }

        // GET: DetalleDiagnosticoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DetalleDiagnosticos == null)
            {
                return NotFound();
            }

            var detalleDiagnostico = await _context.DetalleDiagnosticos.FindAsync(id);
            if (detalleDiagnostico == null)
            {
                return NotFound();
            }
            ViewData["Diagnostico"] = new SelectList(_context.Diagnosticos, "Codigo", "Codigo", detalleDiagnostico.Diagnostico);
            ViewData["Resultado"] = new SelectList(_context.Resultados, "Codigo", "Codigo", detalleDiagnostico.Resultado);
            return View(detalleDiagnostico);
        }

        // POST: DetalleDiagnosticoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Tipo,Codigo,Resultado,Diagnostico")] DetalleDiagnostico detalleDiagnostico)
        {
            if (id != detalleDiagnostico.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleDiagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleDiagnosticoExists(detalleDiagnostico.Codigo))
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
            ViewData["Diagnostico"] = new SelectList(_context.Diagnosticos, "Codigo", "Codigo", detalleDiagnostico.Diagnostico);
            ViewData["Resultado"] = new SelectList(_context.Resultados, "Codigo", "Codigo", detalleDiagnostico.Resultado);
            return View(detalleDiagnostico);
        }

        // GET: DetalleDiagnosticoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DetalleDiagnosticos == null)
            {
                return NotFound();
            }

            var detalleDiagnostico = await _context.DetalleDiagnosticos
                .Include(d => d.DiagnosticoNavigation)
                .Include(d => d.ResultadoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (detalleDiagnostico == null)
            {
                return NotFound();
            }

            return View(detalleDiagnostico);
        }

        // POST: DetalleDiagnosticoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DetalleDiagnosticos == null)
            {
                return Problem("Entity set 'consultoriobdContext.DetalleDiagnosticos'  is null.");
            }
            var detalleDiagnostico = await _context.DetalleDiagnosticos.FindAsync(id);
            if (detalleDiagnostico != null)
            {
                _context.DetalleDiagnosticos.Remove(detalleDiagnostico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleDiagnosticoExists(string id)
        {
          return (_context.DetalleDiagnosticos?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
