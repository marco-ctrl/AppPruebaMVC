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
    public class RolUsuario1Controller : Controller
    {
        private readonly consultoriobdContext _context;

        public RolUsuario1Controller(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: RolUsuario1
        public async Task<IActionResult> Index()
        {
            var consultoriobdContext = _context.RolUsuarios1.Include(r => r.FaurcodrouNavigation).Include(r => r.FaurcodusuNavigation);
            return View(await consultoriobdContext.ToListAsync());
        }

        // GET: RolUsuario1/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.RolUsuarios1 == null)
            {
                return NotFound();
            }

            var rolUsuario1 = await _context.RolUsuarios1
                .Include(r => r.FaurcodrouNavigation)
                .Include(r => r.FaurcodusuNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (rolUsuario1 == null)
            {
                return NotFound();
            }

            return View(rolUsuario1);
        }

        // GET: RolUsuario1/Create
        public IActionResult Create()
        {
            ViewData["Faurcodrou"] = new SelectList(_context.RolUsuarios, "Codigo", "Codigo");
            ViewData["Faurcodusu"] = new SelectList(_context.Usuarios, "Codigo", "Codigo");
            return View();
        }

        // POST: RolUsuario1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Faurcodrou,Faurcodusu")] RolUsuario1 rolUsuario1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolUsuario1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Faurcodrou"] = new SelectList(_context.RolUsuarios, "Codigo", "Codigo", rolUsuario1.Faurcodrou);
            ViewData["Faurcodusu"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", rolUsuario1.Faurcodusu);
            return View(rolUsuario1);
        }

        // GET: RolUsuario1/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.RolUsuarios1 == null)
            {
                return NotFound();
            }

            var rolUsuario1 = await _context.RolUsuarios1.FindAsync(id);
            if (rolUsuario1 == null)
            {
                return NotFound();
            }
            ViewData["Faurcodrou"] = new SelectList(_context.RolUsuarios, "Codigo", "Codigo", rolUsuario1.Faurcodrou);
            ViewData["Faurcodusu"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", rolUsuario1.Faurcodusu);
            return View(rolUsuario1);
        }

        // POST: RolUsuario1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Faurcodrou,Faurcodusu")] RolUsuario1 rolUsuario1)
        {
            if (id != rolUsuario1.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolUsuario1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolUsuario1Exists(rolUsuario1.Codigo))
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
            ViewData["Faurcodrou"] = new SelectList(_context.RolUsuarios, "Codigo", "Codigo", rolUsuario1.Faurcodrou);
            ViewData["Faurcodusu"] = new SelectList(_context.Usuarios, "Codigo", "Codigo", rolUsuario1.Faurcodusu);
            return View(rolUsuario1);
        }

        // GET: RolUsuario1/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.RolUsuarios1 == null)
            {
                return NotFound();
            }

            var rolUsuario1 = await _context.RolUsuarios1
                .Include(r => r.FaurcodrouNavigation)
                .Include(r => r.FaurcodusuNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (rolUsuario1 == null)
            {
                return NotFound();
            }

            return View(rolUsuario1);
        }

        // POST: RolUsuario1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RolUsuarios1 == null)
            {
                return Problem("Entity set 'consultoriobdContext.RolUsuarios1'  is null.");
            }
            var rolUsuario1 = await _context.RolUsuarios1.FindAsync(id);
            if (rolUsuario1 != null)
            {
                _context.RolUsuarios1.Remove(rolUsuario1);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolUsuario1Exists(int id)
        {
          return _context.RolUsuarios1.Any(e => e.Codigo == id);
        }
    }
}
