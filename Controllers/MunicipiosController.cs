using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuLote.Data;
using TuLote.Models;
using TuLote.Servicios;

namespace TuLote.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class MunicipiosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServicio_API_Provincia _API_Provincia;
        private readonly IServicio_API_Municipio _API_Municipio;

        public MunicipiosController(ApplicationDbContext context, IServicio_API_Provincia API_Provincia, IServicio_API_Municipio API_Municipio)
        {
            _context = context;
            _API_Provincia = API_Provincia;
            _API_Municipio = API_Municipio;
        }
        public async Task<IActionResult> Index()
        {

            var lista = await _context.Municipios.Include(p => p.Provincia).ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Provincias = _API_Provincia.Lista().Result.OrderBy(p => p.Nombre);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Municipio municipio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(municipio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("El municipio ya se encuentra registrado");
            }

            var lista = await _context.Municipios.Include(p => p.Provincia).ToListAsync();
            return View(municipio);
        }

        public JsonResult GetMunicipiosById(int idProvincia)
        {
            List<Municipio> municipios = new List<Municipio>();

            municipios = _API_Municipio.Lista().Result.Where(x => x.Provincia.Id == idProvincia).OrderBy(x => x.Nombre).ToList();
            municipios.Insert(0, new Municipio
            {
                Id = 0,
                Nombre = "Por favor seleccione un municipio"
            });
            return Json(municipios);
        }
    }
}
