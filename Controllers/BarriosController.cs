using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuLote.Data;
using TuLote.Models;
using TuLote.Servicios;

namespace TuLote.Controllers
{

    public class BarriosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServicio_API_Localidad _API_Localidad;
        private readonly IServicio_API_Municipio _API_Municipio;

        public BarriosController(ApplicationDbContext context, IServicio_API_Localidad API_localidad, IServicio_API_Municipio API_Municipio)
        {
            _context = context;
            _API_Localidad = API_localidad;
            _API_Municipio = API_Municipio;
        }

        // GET: Barrios
        public async Task<IActionResult> Index()
        {
            var listaBarrios = await _context.Barrios.Include(b => b.Localidad).Include(m => m.Localidad.Municipio).ToListAsync();
            return View(listaBarrios);
        }

        // GET: Barrios/Create
        public IActionResult Create()
        {
            ViewBag.listaMunicipios = _API_Municipio.Lista().Result.OrderBy(m => m.Nombre).ToList();
            return View();
        }

        // POST: Barrios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barrio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Localidad_Id"] = new SelectList(_context.Localidades, "Id", "Nombre", barrio.Localidad_Id);
            ViewBag.listaMunicipios = _API_Municipio.Lista().Result.OrderBy(m => m.Nombre).ToList();
            return View(barrio);
        }

        public JsonResult GetLocalidadesById(int idMunicipio)
        {
            List<Localidad> localidades = new List<Localidad>();

            localidades = _API_Localidad.Lista().Result.Where(x => x.Municipio.Id == idMunicipio).OrderBy(l => l.Nombre).ToList();
            localidades.Insert(0, new Localidad
            {
                Id = 0,
                Nombre = "Por favor seleccione un municipio"
            });

            return Json(localidades);
        }

        // GET: Barrios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Barrios == null)
            {
                return NotFound();
            }

            var barrio = await _context.Barrios
                .Include(b => b.Localidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barrio == null)
            {
                return NotFound();
            }

            return View(barrio);
        }


        // GET: Barrios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Barrios == null)
            {
                return NotFound();
            }

            var barrio = await _context.Barrios.FindAsync(id);
            if (barrio == null)
            {
                return NotFound();
            }
            ViewData["Localidad_Id"] = new SelectList(_context.Localidades, "Id", "Nombre", barrio.Localidad_Id);
            return View(barrio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Barrio barrio)
        {
            if (id != barrio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barrio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarrioExists(barrio.Id))
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
            ViewData["Localidad_Id"] = new SelectList(_context.Localidades, "Id", "Nombre", barrio.Localidad_Id);
            return View(barrio);
        }

        // GET: Barrios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Barrios == null)
            {
                return NotFound();
            }

            var barrio = await _context.Barrios
                .Include(b => b.Localidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barrio == null)
            {
                return NotFound();
            }

            return View(barrio);
        }

        // POST: Barrios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Barrios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Barrios'  is null.");
            }
            var barrio = await _context.Barrios.FindAsync(id);
            if (barrio != null)
            {
                _context.Barrios.Remove(barrio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarrioExists(int id)
        {
            return _context.Barrios.Any(e => e.Id == id);
        }
    }
}
