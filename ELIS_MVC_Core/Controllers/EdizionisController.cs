using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELIS_MVC_Core.Models;

namespace ELIS_MVC_Core.Controllers
{
    public class EdizionisController : Controller
    {
        private readonly CorsiContext _context;

        public EdizionisController(CorsiContext context)
        {
            _context = context;
        }

        // GET: Edizionis
        public async Task<IActionResult> Index()
        {
            var corsiContext = _context.Edizionis.Include(e => e.IdcorsoNavigation);
            return View(await corsiContext.ToListAsync());
        }

        // GET: Edizionis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Edizionis == null)
            {
                return NotFound();
            }

            var edizioni = await _context.Edizionis
                .Include(e => e.IdcorsoNavigation)
                .FirstOrDefaultAsync(m => m.Idedizione == id);
            if (edizioni == null)
            {
                return NotFound();
            }

            return View(edizioni);
        }

        // GET: Edizionis/Create
        public IActionResult Create()
        {
            ViewData["Idcorso"] = new SelectList(_context.Corsis, "Idcorso", "Titolo");
            return View();
        }

        // POST: Edizionis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idedizione,Idcorso,DataInizio,Luogo")] Edizioni edizioni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(edizioni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcorso"] = new SelectList(_context.Corsis, "Idcorso", "Idcorso", edizioni.Idcorso);
            return View(edizioni);
        }

        // GET: Edizionis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Edizionis == null)
            {
                return NotFound();
            }

            var edizioni = await _context.Edizionis.FindAsync(id);
            if (edizioni == null)
            {
                return NotFound();
            }
            ViewData["Idcorso"] = new SelectList(_context.Corsis, "Idcorso", "Idcorso", edizioni.Idcorso);
            return View(edizioni);
        }

        // POST: Edizionis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idedizione,Idcorso,DataInizio,Luogo")] Edizioni edizioni)
        {
            if (id != edizioni.Idedizione)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(edizioni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EdizioniExists(edizioni.Idedizione))
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
            ViewData["Idcorso"] = new SelectList(_context.Corsis, "Idcorso", "Idcorso", edizioni.Idcorso);
            return View(edizioni);
        }

        // GET: Edizionis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Edizionis == null)
            {
                return NotFound();
            }

            var edizioni = await _context.Edizionis
                .Include(e => e.IdcorsoNavigation)
                .FirstOrDefaultAsync(m => m.Idedizione == id);
            if (edizioni == null)
            {
                return NotFound();
            }

            return View(edizioni);
        }

        // POST: Edizionis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Edizionis == null)
            {
                return Problem("Entity set 'CorsiContext.Edizionis'  is null.");
            }
            var edizioni = await _context.Edizionis.FindAsync(id);
            if (edizioni != null)
            {
                _context.Edizionis.Remove(edizioni);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EdizioniExists(int id)
        {
          return (_context.Edizionis?.Any(e => e.Idedizione == id)).GetValueOrDefault();
        }
    }
}
