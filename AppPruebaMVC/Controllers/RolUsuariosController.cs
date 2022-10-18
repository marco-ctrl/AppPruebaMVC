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
    public class RolUsuariosController : Controller
    {
        private readonly consultoriobdContext _context;

        public RolUsuariosController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: RolUsuarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.RolUsuarios.ToListAsync());
        }

        // GET: RolUsuarios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.RolUsuarios == null)
            {
                return NotFound();
            }

            var rolUsuario = await _context.RolUsuarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (rolUsuario == null)
            {
                return NotFound();
            }

            return View(rolUsuario);
        }

        // GET: RolUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RolUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rol,Codigo")] RolUsuario rolUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rolUsuario);
        }

        // GET: RolUsuarios/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.RolUsuarios == null)
            {
                return NotFound();
            }

            var rolUsuario = await _context.RolUsuarios.FindAsync(id);
            if (rolUsuario == null)
            {
                return NotFound();
            }
            return View(rolUsuario);
        }

        // POST: RolUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rol,Codigo")] RolUsuario rolUsuario)
        {
            if (id != rolUsuario.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolUsuarioExists(rolUsuario.Codigo))
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
            return View(rolUsuario);
        }

        // GET: RolUsuarios/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.RolUsuarios == null)
            {
                return NotFound();
            }

            var rolUsuario = await _context.RolUsuarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (rolUsuario == null)
            {
                return NotFound();
            }

            return View(rolUsuario);
        }

        // POST: RolUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RolUsuarios == null)
            {
                return Problem("Entity set 'consultoriobdContext.RolUsuarios'  is null.");
            }
            var rolUsuario = await _context.RolUsuarios.FindAsync(id);
            if (rolUsuario != null)
            {
                _context.RolUsuarios.Remove(rolUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolUsuarioExists(int id)
        {
          return _context.RolUsuarios.Any(e => e.Codigo == id);
        }
    }
}
