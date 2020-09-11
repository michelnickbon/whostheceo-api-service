using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiService.Models;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CeosController : ControllerBase
    {
        private readonly MainDBContext _context;

        public CeosController(MainDBContext context)
        {
            _context = context;
        }

        // GET: api/Ceos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ceo>>> GetCeo()
        {
            return await _context.Ceo.ToListAsync();
        }

        // GET: api/Ceos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ceo>> GetCeo(int id)
        {
            var ceo = await _context.Ceo.FindAsync(id);

            if (ceo == null)
            {
                return NotFound();
            }

            return ceo;
        }

        // PUT: api/Ceos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCeo(int id, Ceo ceo)
        {
            if (id != ceo.CeoId)
            {
                return BadRequest();
            }

            _context.Entry(ceo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CeoExists(id))
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

        // POST: api/Ceos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ceo>> PostCeo(Ceo ceo)
        {
            _context.Ceo.Add(ceo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CeoExists(ceo.CeoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCeo", new { id = ceo.CeoId }, ceo);
        }

        // DELETE: api/Ceos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ceo>> DeleteCeo(int id)
        {
            var ceo = await _context.Ceo.FindAsync(id);
            if (ceo == null)
            {
                return NotFound();
            }

            _context.Ceo.Remove(ceo);
            await _context.SaveChangesAsync();

            return ceo;
        }

        private bool CeoExists(int id)
        {
            return _context.Ceo.Any(e => e.CeoId == id);
        }
    }
}
