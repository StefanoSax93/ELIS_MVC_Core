using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ELIS_MVC_CORE_WebAPI.Models;

namespace ELIS_MVC_CORE_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartecipantisController : ControllerBase
    {
        private readonly CorsiContext _context;

        public PartecipantisController(CorsiContext context)
        {
            _context = context;
        }

        // GET: api/Partecipantis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partecipanti>>> GetPartecipantis()
        {
          if (_context.Partecipantis == null)
          {
              return NotFound();
          }
            return await _context.Partecipantis.ToListAsync();
        }

        // GET: api/Partecipantis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partecipanti>> GetPartecipanti(int id)
        {
          if (_context.Partecipantis == null)
          {
              return NotFound();
          }
            var partecipanti = await _context.Partecipantis.FindAsync(id);

            if (partecipanti == null)
            {
                return NotFound();
            }

            return partecipanti;
        }

        // PUT: api/Partecipantis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartecipanti(int id, Partecipanti partecipanti)
        {
            if (id != partecipanti.Idpartecipante)
            {
                return BadRequest();
            }

            _context.Entry(partecipanti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartecipantiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Partecipantis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partecipanti>> PostPartecipanti(Partecipanti partecipanti)
        {
          if (_context.Partecipantis == null)
          {
              return Problem("Entity set 'CorsiContext.Partecipantis'  is null.");
          }
            _context.Partecipantis.Add(partecipanti);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PartecipantiExists(partecipanti.Idpartecipante))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPartecipanti", new { id = partecipanti.Idpartecipante }, partecipanti);
        }

        // DELETE: api/Partecipantis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartecipanti(int id)
        {
            if (_context.Partecipantis == null)
            {
                return NotFound();
            }
            var partecipanti = await _context.Partecipantis.FindAsync(id);
            if (partecipanti == null)
            {
                return NotFound();
            }

            _context.Partecipantis.Remove(partecipanti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartecipantiExists(int id)
        {
            return (_context.Partecipantis?.Any(e => e.Idpartecipante == id)).GetValueOrDefault();
        }
    }
}
