using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiService.Models;
using ApiService.Dtos;
using AutoMapper;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly MainDBContext _context;
        private readonly IMapper _mapper;

        public HistoryController(MainDBContext context, IMapper mapper)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/History
        /// Returns full history
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetHistory()
        {
            try
            {
                return await _context.History.Include(h => h.Company).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GET: api/History/5
        /// Get a specific history
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetHistory(int id)
        {
            try
            {
                var history = await _context.History.FindAsync(id);
                if (history == null)
                {
                    return NotFound();
                }
                return history;
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// PUT: api/History/5
        /// PUT a specific history
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory(int id, History history)
        {
            if (id != history.HistoryId)
            {
                return BadRequest();
            }

            _context.Entry(history).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryExists(id))
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
        /// POST: api/History
        /// POST a new history
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<History>> PostHistory(HistoryPostDto history)
        {
            var entity = _mapper.Map<History>(history);
            _context.History.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HistoryExists(history.HistoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHistory", new { id = history.HistoryId }, history);
        }

        /// <summary>
        /// DELETE: api/History/5
        /// DELETE a specific history
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<History>> DeleteHistory(int id)
        {
            try
            {
                var history = await _context.History.FindAsync(id);
                if (history == null)
                {
                    return NotFound();
                }
                _context.History.Remove(history);
                await _context.SaveChangesAsync();
                return history;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool HistoryExists(int id)
        {
            return _context.History.Any(e => e.HistoryId == id);
        }
    }
}
