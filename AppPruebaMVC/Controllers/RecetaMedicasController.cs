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
    public class RecetaMedicasController : Controller
    {
        private readonly consultoriobdContext _context;

        public RecetaMedicasController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: RecetaMedicas
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.RecetaMedicas.Include(r => r.CodMedicamentoNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: RecetaMedicas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.RecetaMedicas == null)
            {
                return NotFound();
            }

            var recetaMedica = await _context.RecetaMedicas
                .Include(r => r.CodMedicamentoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (recetaMedica == null)
            {
                return NotFound();
            }

            return View(recetaMedica);
        }

        // GET: RecetaMedicas/Create
        public IActionResult Create()
        {
            ViewData["CodMedicamento"] = new SelectList(_context.Medicamentos, "Codigo", "Codigo");
            return View();
        }

        // POST: RecetaMedicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cantidad,Dosis,Duracion,CodMedicamento,Codigo")] RecetaMedica recetaMedica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recetaMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodMedicamento"] = new SelectList(_context.Medicamentos, "Codigo", "Codigo", recetaMedica.CodMedicamento);
            return View(recetaMedica);
        }

        // GET: RecetaMedicas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.RecetaMedicas == null)
            {
                return NotFound();
            }

            var recetaMedica = await _context.RecetaMedicas.FindAsync(id);
            if (recetaMedica == null)
            {
                return NotFound();
            }
            ViewData["CodMedicamento"] = new SelectList(_context.Medicamentos, "Codigo", "Codigo", recetaMedica.CodMedicamento);
            return View(recetaMedica);
        }

        // POST: RecetaMedicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cantidad,Dosis,Duracion,CodMedicamento,Codigo")] RecetaMedica recetaMedica)
        {
            if (id != recetaMedica.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recetaMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecetaMedicaExists(recetaMedica.Codigo))
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
            ViewData["CodMedicamento"] = new SelectList(_context.Medicamentos, "Codigo", "Codigo", recetaMedica.CodMedicamento);
            return View(recetaMedica);
        }

        // GET: RecetaMedicas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.RecetaMedicas == null)
            {
                return NotFound();
            }

            var recetaMedica = await _context.RecetaMedicas
                .Include(r => r.CodMedicamentoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (recetaMedica == null)
            {
                return NotFound();
            }

            return View(recetaMedica);
        }

        // POST: RecetaMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RecetaMedicas == null)
            {
                return Problem("Entity set 'consultoriobdContext.RecetaMedicas'  is null.");
            }
            var recetaMedica = await _context.RecetaMedicas.FindAsync(id);
            if (recetaMedica != null)
            {
                _context.RecetaMedicas.Remove(recetaMedica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecetaMedicaExists(int id)
        {
          return _context.RecetaMedicas.Any(e => e.Codigo == id);
        }
    }
}
