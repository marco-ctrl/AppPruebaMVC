using AppPruebaMVC.Data.Context;
using AppPruebaMVC.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppPruebaMVC.Controllers
{
    public class PersonasController : Controller
    {
        private readonly consultoriobdContext _context;

        public PersonasController(consultoriobdContext context)
        {
            _context = context;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {

            var _persona = await _context.Personas.OrderByDescending(c => c.Codigo).ToListAsync();

            return _context.Personas != null ?
            View(_persona) :
            Problem("Entity set 'consultoriobdContext.Personas'  is null.");
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            /*var Geners = new Persona
            {
                Generos = new List<Genero>
                {
                    new Genero{Id = true, RoleName = "Masculino"},
                    new Genero {Id = false, RoleName = "Femenino"},
                }
            };*/
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApellidoMaterno,ApellidoPaterno,NumeroCelular,Cedula,Direccion,FechaNacimiento,Estado,EstadoCivil,Nombre,NumeroTelefono,Sexo")] Persona persona)
        {
            persona.Nombre = persona.Nombre.ToUpper();
            persona.ApellidoPaterno = persona.ApellidoPaterno.ToUpper();
            persona.ApellidoMaterno = persona.ApellidoMaterno.ToUpper();
            persona.Direccion = persona.Direccion.ToUpper();

            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApellidoMaterno,ApellidoPaterno,NumeroCelular,Cedula,Direccion,FechaNacimiento,Estado,EstadoCivil,Nombre,NumeroTelefono,Sexo,Codigo")] Persona persona)
        {
            if (id != persona.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    persona.Nombre = persona.Nombre.ToUpper();
                    persona.ApellidoPaterno = persona.ApellidoPaterno.ToUpper();
                    persona.ApellidoMaterno = persona.ApellidoMaterno.ToUpper();
                    persona.Direccion = persona.Direccion.ToUpper();

                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Codigo))
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
            return View(persona);
        }

        // GET: Personas/Baja/5
        public async Task<IActionResult> Baja(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return PartialView("Baja", persona);
        }

        [HttpPost, ActionName("DarBaja")]
        public async Task<IActionResult> ConfirmarBaja([Bind("ApellidoMaterno,ApellidoPaterno,NumeroCelular,Cedula,Direccion,FechaNacimiento,Estado,EstadoCivil,Nombre,NumeroTelefono,Sexo,Codigo")] Persona persona)
        {
            /*if (id != persona.Codigo)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Codigo))
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
            return Json(persona);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [Bind("Estado, Codigo")] Persona persona)
        {
            if (_context.Personas == null)
            {
                return Problem("Entity set 'consultoriobdContext.TipoUsuarios'  is null.");
            }
            var tipoUsuario = await _context.Personas.FindAsync(id);
            if (tipoUsuario != null)
            {
                _context.Personas.Update(tipoUsuario);
            }

            await _context.SaveChangesAsync();
            return Ok(tipoUsuario);
            //return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int? id)
        {
            return (_context.Personas?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
