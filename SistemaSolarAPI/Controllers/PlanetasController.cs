﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSolarAPI.Data;
using SistemaSolarAPI.Entities;

namespace SistemaSolarAPI.Controllers
{
    [Route("api/solarsystem")]
    [ApiController]
    public class PlanetasController : ControllerBase
    {
        private readonly SistemaSolarAPIContext _context;

        public PlanetasController(SistemaSolarAPIContext context)
        {
            _context = context;
        }

        // GET: api/Planetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Planeta>>> GetPlaneta()
        {
            return await _context.Planeta.ToListAsync();
        }

        // GET: api/Planetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Planeta>> GetPlaneta(int id)
        {
            var planeta = await _context.Planeta.FindAsync(id);

            if (planeta == null)
            {
                return NotFound();
            }

            return planeta;
        }

        // PUT: api/Planetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaneta(int id, Planeta planeta)
        {
            if (id != planeta.Id)
            {
                return BadRequest();
            }

            _context.Entry(planeta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanetaExists(id))
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

        // POST: api/Planetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Planeta>> PostPlaneta(Planeta planeta)
        {
            _context.Planeta.Add(planeta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaneta", new { id = planeta.Id }, planeta);
        }

        // DELETE: api/Planetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaneta(int id)
        {
            var planeta = await _context.Planeta.FindAsync(id);
            if (planeta == null)
            {
                return NotFound();
            }

            _context.Planeta.Remove(planeta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanetaExists(int id)
        {
            return _context.Planeta.Any(e => e.Id == id);
        }
    }
}
