using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassroomScheduler.Models;

namespace ClassroomScheduler.Controllers
{
    [Produces("application/json")]
    [Route("api/EventRepetitions")]
    public class EventRepetitionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventRepetitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventRepetitions
        [HttpGet]
        public IEnumerable<EventRepetition> GetEventRepetitions()
        {
            return _context.EventRepetitions;
        }

        // GET: api/EventRepetitions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventRepetition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventRepetition = await _context.EventRepetitions.SingleOrDefaultAsync(m => m.Id == id);

            if (eventRepetition == null)
            {
                return NotFound();
            }

            return Ok(eventRepetition);
        }

        // PUT: api/EventRepetitions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventRepetition([FromRoute] int id, [FromBody] EventRepetition eventRepetition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventRepetition.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventRepetition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventRepetitionExists(id))
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

        // POST: api/EventRepetitions
        [HttpPost]
        public async Task<IActionResult> PostEventRepetition([FromBody] EventRepetition eventRepetition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EventRepetitions.Add(eventRepetition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventRepetition", new { id = eventRepetition.Id }, eventRepetition);
        }

        // DELETE: api/EventRepetitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventRepetition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventRepetition = await _context.EventRepetitions.SingleOrDefaultAsync(m => m.Id == id);
            if (eventRepetition == null)
            {
                return NotFound();
            }

            _context.EventRepetitions.Remove(eventRepetition);
            await _context.SaveChangesAsync();

            return Ok(eventRepetition);
        }

        private bool EventRepetitionExists(int id)
        {
            return _context.EventRepetitions.Any(e => e.Id == id);
        }
    }
}