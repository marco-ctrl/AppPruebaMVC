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
    public class AdmicionsController : Controller
    {
        private readonly consultoriobdContext _context;

        public AdmicionsController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Admicions
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.Admicions.Include(a => a.CodEnfermeraNavigation).Include(a => a.CodPacienteNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: Admicions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Admicions == null)
            {
                return NotFound();
            }

            var admicion = await _context.Admicions
                .Include(a => a.CodEnfermeraNavigation)
                .Include(a => a.CodPacienteNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (admicion == null)
            {
                return NotFound();
            }

            return View(admicion);
        }

        // GET: Admicions/Create
        public IActionResult Create()
        {
            ViewData["CodEnfermera"] = new SelectList(_context.Enfermeras, "Codigo", "Codigo");
            ViewData["CodPaciente"] = new SelectList(_context.Pacientes, "Codigo", "Codigo");
            return View();
        }

        // POST: Admicions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FrecuenciaCardiaca,FrecuenciaRespiratoria,Ocupacion,Peso,PresionArterial,Saturacion,Temperatura,Codigo,CodPaciente,CodEnfermera")] Admicion admicion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admicion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodEnfermera"] = new SelectList(_context.Enfermeras, "Codigo", "Codigo", admicion.CodEnfermera);
            ViewData["CodPaciente"] = new SelectList(_context.Pacientes, "Codigo", "Codigo", admicion.CodPaciente);
            return View(admicion);
        }

        // GET: Admicions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Admicions == null)
            {
                return NotFound();
            }

            var admicion = await _context.Admicions.FindAsync(id);
            if (admicion == null)
            {
                return NotFound();
            }
            ViewData["CodEnfermera"] = new SelectList(_context.Enfermeras, "Codigo", "Codigo", admicion.CodEnfermera);
            ViewData["CodPaciente"] = new SelectList(_context.Pacientes, "Codigo", "Codigo", admicion.CodPaciente);
            return View(admicion);
        }

        // POST: Admicions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FrecuenciaCardiaca,FrecuenciaRespiratoria,Ocupacion,Peso,PresionArterial,Saturacion,Temperatura,Codigo,CodPaciente,CodEnfermera")] Admicion admicion)
        {
            if (id != admicion.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admicion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmicionExists(admicion.Codigo))
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
            ViewData["CodEnfermera"] = new SelectList(_context.Enfermeras, "Codigo", "Codigo", admicion.CodEnfermera);
            ViewData["CodPaciente"] = new SelectList(_context.Pacientes, "Codigo", "Codigo", admicion.CodPaciente);
            return View(admicion);
        }

        // GET: Admicions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Admicions == null)
            {
                return NotFound();
            }

            var admicion = await _context.Admicions
                .Include(a => a.CodEnfermeraNavigation)
                .Include(a => a.CodPacienteNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (admicion == null)
            {
                return NotFound();
            }

            return View(admicion);
        }

        // POST: Admicions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Admicions == null)
            {
                return Problem("Entity set 'consultoriobdContext.Admicions'  is null.");
            }
            var admicion = await _context.Admicions.FindAsync(id);
            if (admicion != null)
            {
                _context.Admicions.Remove(admicion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmicionExists(int id)
        {
          return _context.Admicions.Any(e => e.Codigo == id);
        }
    }
}
