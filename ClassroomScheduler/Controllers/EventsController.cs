using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassroomScheduler.Models;
using ClassroomScheduler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ClassroomScheduler.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Events
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Event> GetEvents()
        {
            return _context.Events.Include(e => e.EventType)
                .Include(cr => cr.ClassRoom)
                .Include(c => c.Course);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Events.Include(e => e.EventType)
                .Include(cr => cr.ClassRoom)
                .Include(c => c.Course)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userName = HttpContext.User.Claims.FirstOrDefault().Value;

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound();
            }

            var @event = new Event
            {
                Id = id,
                Description = model.Description,
                EventType = _context.EventTypes.Where(e => e.Id == model.EventTypeId).First(),
                ClassRoom = _context.ClassRooms.Where(cr => cr.Id == model.ClassRoomId).First(),
                Course = _context.Courses.Where(c => c.Id == model.CourseId).FirstOrDefault(),
                CreatedBy = user
            };

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userName = HttpContext.User.Claims.FirstOrDefault().Value;

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound();
            }

            var @event = new Event
            { 
                Description = model.Description,
                EventType = _context.EventTypes.Where(e => e.Id == model.EventTypeId).First(),
                ClassRoom = _context.ClassRooms.Where(cr => cr.Id == model.ClassRoomId).First(),
                Course = _context.Courses.Where(c => c.Id == model.CourseId).FirstOrDefault(),
                CreatedBy = user
            };

            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return Ok(@event);
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}