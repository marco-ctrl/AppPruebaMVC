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
            var consultoriobdContext = _context.Resultados.Include(r => r.CodigoCitaNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Resultadoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Resultados == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados
                .Include(r => r.CodigoCitaNavigation)
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
            ViewData["CodigoCita"] = new SelectList(_context.Cita, "Codigo", "Codigo");
            return View();
        }

        // POST: Resultadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Antecedentes,Estado,Fecha,Motivo,ProximaCita,TiempoEnfermedad,Tratamiento,Codigo,CodigoCita")] Resultado resultado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCita"] = new SelectList(_context.Cita, "Codigo", "Codigo", resultado.CodigoCita);
            return View(resultado);
        }

        // GET: Resultadoes/Edit/5
        public async Task<IActionResult> Edit(string id)
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
            ViewData["CodigoCita"] = new SelectList(_context.Cita, "Codigo", "Codigo", resultado.CodigoCita);
            return View(resultado);
        }

        // POST: Resultadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Antecedentes,Estado,Fecha,Motivo,ProximaCita,TiempoEnfermedad,Tratamiento,Codigo,CodigoCita")] Resultado resultado)
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
            ViewData["CodigoCita"] = new SelectList(_context.Cita, "Codigo", "Codigo", resultado.CodigoCita);
            return View(resultado);
        }

        // GET: Resultadoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Resultados == null)
            {
                return NotFound();
            }

            var resultado = await _context.Resultados
                .Include(r => r.CodigoCitaNavigation)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
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

        private bool ResultadoExists(string id)
        {
            return (_context.Resultados?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
