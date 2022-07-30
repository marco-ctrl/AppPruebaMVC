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
    public class TipoUsuariosController : Controller
    {
        private readonly consultoriobdContext _context;

        public TipoUsuariosController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: TipoUsuarios
        public async Task<IActionResult> Index()
        {
              return _context.TipoUsuarios != null ? 
                          View(await _context.TipoUsuarios.ToListAsync()) :
                          Problem("Entity set 'consultoriobdContext.TipoUsuarios'  is null.");
        }

        // GET: TipoUsuarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TipoUsuarios == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.TipoUsuarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cargo,Codigo")] TipoUsuario tipoUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TipoUsuarios == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.TipoUsuarios.FindAsync(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }
            return View(tipoUsuario);
        }

        // POST: TipoUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cargo,Codigo")] TipoUsuario tipoUsuario)
        {
            if (id != tipoUsuario.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoUsuarioExists(tipoUsuario.Codigo))
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
            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Delete/5
        /*public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TipoUsuarios == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.TipoUsuarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return View(tipoUsuario);
        }*/

        // POST: TipoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TipoUsuarios == null)
            {
                return Problem("Entity set 'consultoriobdContext.TipoUsuarios'  is null.");
            }
            var tipoUsuario = await _context.TipoUsuarios.FindAsync(id);
            if (tipoUsuario != null)
            {
                _context.TipoUsuarios.Remove(tipoUsuario);
            }
            
            await _context.SaveChangesAsync();
            return Ok(tipoUsuario);
            //return RedirectToAction(nameof(Index));
        }

        private bool TipoUsuarioExists(string id)
        {
          return (_context.TipoUsuarios?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
