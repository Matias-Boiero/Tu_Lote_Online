using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using TuLote.Data;
using TuLote.Models;

namespace TuLote.Controllers
{
    public class LotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listaLotes = _context.Lotes.Include(b => b.Barrio).Include(l => l.Barrio.Localidad).Include(l => l.Usuario).ToList();
            return View(listaLotes);
        }

        //Get
        public async Task<IActionResult> Crear()
        {
            ViewData["Barrio_Id"] = new SelectList(_context.Barrios, "Id", "Nombre");
            ViewData["Usuario"] = new SelectList(_context.Users, "Id", "Alias");

            return View();
        }

        //Post
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Crear(Lote lote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lote);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Barrio_Id"] = new SelectList(_context.Barrios, "Id", "Nombre");
            return (View(lote));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lotes == null)
            {
                return NotFound();
            }

            var lote = await _context.Lotes
                .Include(b => b.Barrio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lotes == null)
            {
                return NotFound();
            }

            var lote = await _context.Lotes.FindAsync(id);
            if (lote == null)
            {
                return NotFound();
            }
            ViewData["Barrio_Id"] = new SelectList(_context.Barrios, "Id", "Nombre", lote.Barrio_Id);
            ViewData["Usuario"] = new SelectList(_context.Users, "Id", "Alias");
            return View(lote);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Lote lote)
        {
            if (id != lote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarrioExists(lote.Id))
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
            ViewData["Barrio_Id"] = new SelectList(_context.Barrios, "Id", "Nombre", lote.Barrio_Id);
            return View(lote);
        }

        // GET: Barrios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lotes == null)
            {
                return NotFound();
            }

            var lote = await _context.Lotes
                .Include(b => b.Barrio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lote == null)
            {
                return NotFound();
            }

            return View(lote);
        }

        // POST: Barrios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lotes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Barrios'  is null.");
            }
            var lote = await _context.Lotes.FindAsync(id);
            if (lote != null)
            {
                _context.Lotes.Remove(lote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarrioExists(int id)
        {
            return _context.Lotes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> LotesPDF()
        {
            //return View(await _context.Lotes.ToListAsync());

            return new ViewAsPdf("LotesPDF", await _context.Lotes.Include(b => b.Barrio).Include(l => l.Barrio.Localidad).Include(l => l.Usuario).ToListAsync())
            {
                PageMargins = new Margins(5, 10, 12, 10)
            };
        }

    }
}

