using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC.Controllers
{
    public class MedicamentoesController : Controller
    {
        private readonly consultoriobdContext _context;

        public MedicamentoesController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Medicamentoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicamentos.ToListAsync());
        }

        // GET: Medicamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicamentos == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (medicamento == null)
            {
                return NotFound();
            }

            return View(medicamento);
        }

        // GET: Medicamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreMedicamento,Codigo,Descripcion,Estado")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                medicamento.NombreMedicamento = medicamento.NombreMedicamento.ToUpper();
                medicamento.Descripcion = medicamento.Descripcion.ToUpper();
                medicamento.Estado = true;
                _context.Add(medicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicamento);
        }

        // GET: Medicamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicamentos == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }
            return View(medicamento);
        }

        // POST: Medicamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NombreMedicamento,Codigo,Descripcion,Estado")] Medicamento medicamento)
        {
            if (id != medicamento.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    medicamento.NombreMedicamento = medicamento.NombreMedicamento.ToUpper();
                    medicamento.Descripcion = medicamento.Descripcion.ToUpper();
                    medicamento.Estado = true;

                    _context.Update(medicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentoExists(medicamento.Codigo))
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
            return View(medicamento);
        }

        // GET: Medicamentoes/Baja/5
        public async Task<IActionResult> Baja(int? id)
        {
            if (id == null || _context.Medicamentos == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }
            return PartialView("Baja", medicamento);
        }

        // POST: Medicamentoes/Delete/5
        [HttpPost, ActionName("DarBaja")]
        public async Task<IActionResult> Baja(int id, [Bind("NombreMedicamento,Codigo,Descripcion,Estado")] Medicamento medicamento)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentoExists(medicamento.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return Json(medicamento);
        }

        private bool MedicamentoExists(int? id)
        {
            return _context.Medicamentos.Any(e => e.Codigo == id);
        }
    }
}
