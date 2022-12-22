using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC.Controllers
{
    public class ResultadoesController : Controller
    {
        private readonly consultoriobdContext _context;

        public ResultadoesController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Resultadoes
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Resultados.Include(r => r.CodCitaNavigation).Include(r => r.CodtratamientoNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Resultadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resultados == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados
                .Include(r => r.CodCitaNavigation)
                .Include(r => r.CodtratamientoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // GET: Resultadoes/Create
        public IActionResult Create()
        {
            ViewData["CodCita"] = new SelectList(_context.CitaMedicas, "Codigo", "Codigo");
            ViewData["CodTratamiento"] = new SelectList(_context.RecetaMedicas, "Codigo", "Codigo");
            return View();
        }

        // POST: Resultadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Antecedentes,Estado,FechaResultado,MotivoConsulta,ProximaCita,TiempoEnfermedad,Tratamiento,Parscodres,Farscodres")] Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodCita"] = new SelectList(_context.CitaMedicas, "Codigo", "Codigo", resultado.CodCitaNavigation);
            ViewData["CodTratamiento"] = new SelectList(_context.RecetaMedicas, "Codigo", "Codigo", resultado.CodtratamientoNavigation);
            return View(resultado);
        }

        // GET: Resultadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resultados == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados.FindAsync(id);
            if (resultado == null)
            {
                return NotFound();
            }
            ViewData["CodCita"] = new SelectList(_context.CitaMedicas, "Codigo", "Codigo", resultado.CodCita);
            return View(resultado);
        }

        // POST: Resultadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Antecedentes,Estado,FechaResultado,MotivoConsulta,ProximaCita,TiempoEnfermedad,Tratamiento,Parscodres,Farscodres")] Resultado resultado)
        {
            if (id != resultado.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultadoExists(resultado.Codigo))
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
            ViewData["CodCita"] = new SelectList(_context.CitaMedicas, "Codigo", "Codigo", resultado.CodCita);
            return View(resultado);
        }

        // GET: Resultadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resultados == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados
                .Include(r => r.CodCitaNavigation)
                .Include(r => r.CodtratamientoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);
        }

        // POST: Resultadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Resultados == null)
            {
                return Problem("Entity set 'consultoriobdContext.Resultados'  is null.");
            }
            var resultado = await _context.Resultados.FindAsync(id);
            if (resultado != null)
            {
                _context.Resultados.Remove(resultado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultadoExists(int? id)
        {
            return _context.Resultados.Any(e => e.Codigo == id);
        }
    }
}
