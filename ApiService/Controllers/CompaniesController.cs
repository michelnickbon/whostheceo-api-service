﻿using System.Collections.Generic;
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
    public class CompaniesController : ControllerBase
    {
        private readonly MainDBContext _context;
        private readonly IMapper _mapper;

        public CompaniesController(MainDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// GET: api/Companies/List
        /// Returns a filtered list of all companies
        /// </summary>
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanyList()
        {
            try
            {
                var companies = await _context.Company.Select(c => new { c.CompanyId, c.CompanyName }).ToListAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GET: api/Companies
        /// Returns all available companies
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
        {
            try
            {
                return await _context.Company.ToListAsync();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GET: api/Companies/5
        /// Get a specific company
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            try
            {
                var company = await _context.Company.Include(c => c.Ceo).FirstOrDefaultAsync(c => c.CompanyId == id);
                if (company == null)
                {
                    return NotFound();
                }
                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// PUT: api/Companies/5
        /// PUT a specific company
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyPutDto company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<Company>(company);

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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
        /// POST: api/Companies
        /// POST a new company
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CompanyPostDto company)
        {
            var entity = _mapper.Map<Company>(company);
            _context.Company.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CompanyExists(company.CompanyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompany", new { id = company.CompanyId }, company);
        }

        /// <summary>
        /// DELETE: api/Companies/5
        /// Delete a company
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            try
            {
                var company = await _context.Company.FindAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                _context.Company.Remove(company);
                await _context.SaveChangesAsync();
                return company;
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.CompanyId == id);
        }
    }
}