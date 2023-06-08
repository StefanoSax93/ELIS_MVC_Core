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
    public class PartecipantisController : Controller
    {
        private readonly CorsiContext _context;

        public PartecipantisController(CorsiContext context)
        {
            _context = context;
        }

        // GET: Partecipantis
        public async Task<IActionResult> Index()
        {
              return _context.Partecipantis != null ? 
                          View(await _context.Partecipantis.ToListAsync()) :
                          Problem("Entity set 'CorsiContext.Partecipantis'  is null.");
        }

        // GET: Partecipantis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Partecipantis == null)
            {
                return NotFound();
            }

            var partecipanti = await _context.Partecipantis
                .FirstOrDefaultAsync(m => m.Idpartecipante == id);
            if (partecipanti == null)
            {
                return NotFound();
            }

            return View(partecipanti);
        }

        // GET: Partecipantis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partecipantis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idpartecipante,Nome,Cognome,DataNascita,Residenza,TitoloStudio")] Partecipanti partecipanti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partecipanti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partecipanti);
        }

        // GET: Partecipantis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Partecipantis == null)
            {
                return NotFound();
            }

            var partecipanti = await _context.Partecipantis.FindAsync(id);
            if (partecipanti == null)
            {
                return NotFound();
            }
            return View(partecipanti);
        }

        // POST: Partecipantis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idpartecipante,Nome,Cognome,DataNascita,Residenza,TitoloStudio")] Partecipanti partecipanti)
        {
            if (id != partecipanti.Idpartecipante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partecipanti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartecipantiExists(partecipanti.Idpartecipante))
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
            return View(partecipanti);
        }

        // GET: Partecipantis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Partecipantis == null)
            {
                return NotFound();
            }

            var partecipanti = await _context.Partecipantis
                .FirstOrDefaultAsync(m => m.Idpartecipante == id);
            if (partecipanti == null)
            {
                return NotFound();
            }

            return View(partecipanti);
        }

        // POST: Partecipantis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Partecipantis == null)
            {
                return Problem("Entity set 'CorsiContext.Partecipantis'  is null.");
            }
            var partecipanti = await _context.Partecipantis.FindAsync(id);
            if (partecipanti != null)
            {
                _context.Partecipantis.Remove(partecipanti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartecipantiExists(int id)
        {
          return (_context.Partecipantis?.Any(e => e.Idpartecipante == id)).GetValueOrDefault();
        }
    }
}
