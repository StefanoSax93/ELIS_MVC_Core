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
    public class CorsisController : ControllerBase
    {
        private readonly CorsiContext _context;

        public CorsisController(CorsiContext context)
        {
            _context = context;
        }
        
        // GET: api/Corsis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Corsi>>> GetCorsis()
        {
          if (_context.Corsis == null)
          {
              return NotFound();
          }
            return await _context.Corsis.ToListAsync();
        }

        // GET: api/Corsis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Corsi>> GetCorsi(int id)
        {
          if (_context.Corsis == null)
          {
              return NotFound();
          }
            var corsi = await _context.Corsis.FindAsync(id);

            if (corsi == null)
            {
                return NotFound();
            }

            return corsi;
        }

        // PUT: api/Corsis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorsi(int id, Corsi corsi)
        {
            if (id != corsi.Idcorso)
            {
                return BadRequest();
            }

            _context.Entry(corsi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorsiExists(id))
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

        // POST: api/Corsis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Corsi>> PostCorsi(Corsi corsi)
        {
          if (_context.Corsis == null)
          {
              return Problem("Entity set 'CorsiContext.Corsis'  is null.");
          }
            _context.Corsis.Add(corsi);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CorsiExists(corsi.Idcorso))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCorsi", new { id = corsi.Idcorso }, corsi);
        }

        // DELETE: api/Corsis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorsi(int id)
        {
            if (_context.Corsis == null)
            {
                return NotFound();
            }
            var corsi = await _context.Corsis.FindAsync(id);
            if (corsi == null)
            {
                return NotFound();
            }

            _context.Corsis.Remove(corsi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CorsiExists(int id)
        {
            return (_context.Corsis?.Any(e => e.Idcorso == id)).GetValueOrDefault();
        }
    }
}
