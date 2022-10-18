﻿using System;
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
    public class CitaMedicasController : Controller
    {
        private readonly consultoriobdContext _context;

        public CitaMedicasController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: CitaMedicas
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.CitaMedicas.Include(c => c.CodDoctorNavigation).Include(c => c.CodEnfermeraNavigation).Include(c => c.CodUsuarioNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: CitaMedicas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.CitaMedicas == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitaMedicas
                .Include(c => c.CodDoctorNavigation)
                .Include(c => c.CodEnfermeraNavigation)
                .Include(c => c.CodUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (citaMedica == null)
            {
                return NotFound();
            }

            return View(citaMedica);
        }

        // GET: CitaMedicas/Create
        public IActionResult Create()
        {
            ViewData["CodDoctor"] = new SelectList(_context.Doctors, "Codigo", "Codigo");
            ViewData["CodEnfermera"] = new SelectList(_context.Enfermeras, "Codigo", "Codigo");
            ViewData["CodUsuario"] = new SelectList(_context.Usuarios, "Codigo", "Codigo");
            return View();
        }

        // POST: CitaMedicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Estado,FechaHora,Codigo,CodUsuario,CodDoctor,CodEnfermera")] CitaMedica citaMedica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citaMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodDoctor"] = new SelectList(_context.Doctors, "Codigo", "Codigo", citaMedica.CodDoctor);
            ViewData["CodEnfermera"] = new SelectList(_context.Enfermeras, "Codigo", "Codigo", citaMedica.CodEnfermera);
            ViewData["CodUsuario"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", citaMedica.CodUsuario);
            return View(citaMedica);
        }

        // GET: CitaMedicas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.CitaMedicas == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitaMedicas.FindAsync(id);
            if (citaMedica == null)
            {
                return NotFound();
            }
            ViewData["CodDoctor"] = new SelectList(_context.Doctors, "Codigo", "Codigo", citaMedica.CodDoctor);
            ViewData["CodEnfermera"] = new SelectList(_context.Enfermeras, "Codigo", "Codigo", citaMedica.CodEnfermera);
            ViewData["CodUsuario"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", citaMedica.CodUsuario);
            return View(citaMedica);
        }

        // POST: CitaMedicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Estado,FechaHora,Codigo,CodUsuario,CodDoctor,CodEnfermera")] CitaMedica citaMedica)
        {
            if (id != citaMedica.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citaMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaMedicaExists(citaMedica.Codigo))
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
            ViewData["CodDoctor"] = new SelectList(_context.Doctors, "Codigo", "Codigo", citaMedica.CodDoctor);
            ViewData["CodEnfermera"] = new SelectList(_context.Enfermeras, "Codigo", "Codigo", citaMedica.CodEnfermera);
            ViewData["CodUsuario"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", citaMedica.CodUsuario);
            return View(citaMedica);
        }

        // GET: CitaMedicas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.CitaMedicas == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitaMedicas
                .Include(c => c.CodDoctorNavigation)
                .Include(c => c.CodEnfermeraNavigation)
                .Include(c => c.CodUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (citaMedica == null)
            {
                return NotFound();
            }

            return View(citaMedica);
        }

        // POST: CitaMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CitaMedicas == null)
            {
                return Problem("Entity set 'consultoriobdContext.CitaMedicas'  is null.");
            }
            var citaMedica = await _context.CitaMedicas.FindAsync(id);
            if (citaMedica != null)
            {
                _context.CitaMedicas.Remove(citaMedica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaMedicaExists(int id)
        {
          return _context.CitaMedicas.Any(e => e.Codigo == id);
        }
    }
}
