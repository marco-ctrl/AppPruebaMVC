using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC.Controllers
{
    public class DiagnosticoesController : Controller
    {
        private readonly consultoriobdContext _context;

        public DiagnosticoesController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Diagnosticoes
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Diagnosticos.Include(d => d.CodEnfermedadNavigation).Include(d => d.CodResultadoNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Diagnosticoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.CodEnfermedadNavigation)
                .Include(d => d.CodResultadoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Diagnosticoes/Create
        public IActionResult Create()
        {
            ViewData["CodEnfermedad"] = new SelectList(_context.Enfermedads, "Codigo", "Codigo");
            ViewData["CodResultado"] = new SelectList(_context.Resultados, "Parscodres", "Parscodres");
            return View();
        }

        // POST: Diagnosticoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoDiagnostico,Codigo,CodResultado,CodEnfermedad")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodEnfermedad"] = new SelectList(_context.Enfermedads, "Codigo", "Codigo", diagnostico.CodEnfermedad);
            ViewData["CodResultado"] = new SelectList(_context.Resultados, "Parscodres", "Parscodres", diagnostico.CodResultado);
            return View(diagnostico);
        }

        // GET: Diagnosticoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            ViewData["CodEnfermedad"] = new SelectList(_context.Enfermedads, "Codigo", "Codigo", diagnostico.CodEnfermedad);
            ViewData["CodResultado"] = new SelectList(_context.Resultados, "Parscodres", "Parscodres", diagnostico.CodResultado);
            return View(diagnostico);
        }

        // POST: Diagnosticoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoDiagnostico,Codigo,CodResultado,CodEnfermedad")] Diagnostico diagnostico)
        {
            if (id != diagnostico.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.Codigo))
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
            ViewData["CodEnfermedad"] = new SelectList(_context.Enfermedads, "Codigo", "Codigo", diagnostico.CodEnfermedad);
            ViewData["CodResultado"] = new SelectList(_context.Resultados, "Parscodres", "Parscodres", diagnostico.CodResultado);
            return View(diagnostico);
        }

        // GET: Diagnosticoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.CodEnfermedadNavigation)
                .Include(d => d.CodResultadoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Diagnosticoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Diagnosticos == null)
            {
                return Problem("Entity set 'consultoriobdContext.Diagnosticos'  is null.");
            }
            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico != null)
            {
                _context.Diagnosticos.Remove(diagnostico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int? id)
        {
            return _context.Diagnosticos.Any(e => e.Codigo == id);
        }
    }
}
