using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiService.Models;
using System;
using ApiService.Dtos;
using AutoMapper;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CeosController : ControllerBase
    {
        private readonly MainDBContext _context;
        private readonly IMapper _mapper;

        public CeosController(MainDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// GET: api/Ceos
        /// Returns all available CEO's
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ceo>>> GetCeo()
        {
            try
            {
                return await _context.Ceo.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GET: api/Ceos/1
        /// Returns a specific CEO specified by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Ceo>> GetCeo(int id)
        {
            try
            {
                var ceo = await _context.Ceo.FindAsync(id);
                if (ceo == null)
                {
                    return NotFound();
                }
                return ceo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// PUT: api/Ceos/1
        /// PUT a specific CEO specified by id
        /// </summary>
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

        /// <summary>
        /// POST: api/Ceos
        /// POST a new CEO
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Ceo>> PostCeo(CeoPostDto ceo)
        {
            var entity = _mapper.Map<Ceo>(ceo);
            _context.Ceo.Add(entity);

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

        /// <summary>
        /// DELETE: api/Ceos/1
        /// Delete a CEO
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ceo>> DeleteCeo(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CeoExists(int id)
        {
            return _context.Ceo.Any(e => e.CeoId == id);
        }
    }
}
