using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TuLote.Data;
using TuLote.Models;
using TuLote.Servicios;

namespace TuLote.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProvinciasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServicio_API_Provincia _API_Provincia;

        public ProvinciasController(ApplicationDbContext context, IServicio_API_Provincia API_Provincia)
        {
            _context = context;
            _API_Provincia = API_Provincia;
        }
        public IActionResult Index()
        {
            var lista = _context.Provincias.ToList();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.listaProvincias = new SelectList(await _API_Provincia.Lista(), "Id", "Nombre").OrderBy(p => p.Text);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provincia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provincia);
        }


    }
}
