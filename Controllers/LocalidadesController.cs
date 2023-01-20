using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuLote.Data;
using TuLote.Models;
using TuLote.Servicios;

namespace TuLote.Controllers
{
    public class LocalidadesController : Controller
    {
        private readonly IServicio_API_Localidad _API_Localidad;
        private readonly IServicio_API_Municipio _API_Municipio;
        private readonly ApplicationDbContext _context;

        public LocalidadesController(IServicio_API_Localidad API_localidad, IServicio_API_Municipio API_Municipio,
            ApplicationDbContext context)
        {
            _API_Localidad = API_localidad;
            _API_Municipio = API_Municipio;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Localidades.Include(m => m.Municipio).ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.listaMunicipios = _API_Municipio.Lista().Result.OrderBy(m => m.Nombre).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Localidad localidad)
        {

            if (ModelState.IsValid)
            {
                _context.Add(localidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(localidad);
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

    }
}
