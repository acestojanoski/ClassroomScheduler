using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ClassroomScheduler.Models;
using ClassroomScheduler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

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
                .Include(c => c.Course)
                .Include(er => er.EventRepetitions);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var @event = await _context.Events.Include(e => e.EventType)
                .Include(cr => cr.ClassRoom)
                .Include(c => c.Course)
                .Include(er => er.EventRepetitions)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (@event == null)
                return NotFound();

            return Ok(@event);
        }

        // PUT: api/Events/5/Repetition/5
        [HttpPut("{id}/Repetition/{EventRepetitionId}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromRoute] int EventRepetitionId, [FromBody] EventEditViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userName = HttpContext.User.Claims.FirstOrDefault().Value;

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return NotFound();
            
            var @event = new Event
            {
                Id = id,
                Description = model.Description,
                EventType = _context.EventTypes.Where(e => e.Id == model.EventTypeId).First(),
                ClassRoom = _context.ClassRooms.Where(cr => cr.Id == model.ClassRoomId).First(),
                Course = _context.Courses.Where(c => c.Id == model.CourseId).FirstOrDefault(),
                CreatedBy = user
            };

            var eventRepetition = _context.EventRepetitions.Where(e => e.Id == EventRepetitionId).FirstOrDefault();
            
            eventRepetition.StartTime = model.StartTime;
            eventRepetition.EndTime = model.EndTime;

            _context.EventRepetitions.Update(eventRepetition);            

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] EventViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userName = HttpContext.User.Claims.FirstOrDefault().Value;

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return NotFound();

            var @event = new Event
            {
                Description = model.Description,
                EventType = _context.EventTypes.Where(e => e.Id == model.EventTypeId).First(),
                ClassRoom = _context.ClassRooms.Where(cr => cr.Id == model.ClassRoomId).First(),
                Course = _context.Courses.Where(c => c.Id == model.CourseId).FirstOrDefault(),
                CreatedBy = user
            };

            List<EventRepetition> eventRepetitions = new List<EventRepetition>();

            if (model.Repeat)
            {
                for (var i = 0; i < model.RepeatTimes; i++)
                {
                    if (model.RepeatInterval == "day")
                    {
                        eventRepetitions.Add(new EventRepetition
                        {
                            Event = @event,
                            StartTime = model.StartTime.AddDays(i),
                            EndTime = model.EndTime.AddDays(i)
                        });
                    }
                    else if (model.RepeatInterval == "week")
                    {
                        eventRepetitions.Add(new EventRepetition
                        {
                            Event = @event,
                            StartTime = model.StartTime.AddDays(7*i),
                            EndTime = model.EndTime.AddDays(7*i)
                        });
                    }
                    else if (model.RepeatInterval == "month")
                    {
                        eventRepetitions.Add(new EventRepetition
                        {
                            Event = @event,
                            StartTime = model.StartTime.AddMonths(i),
                            EndTime = model.EndTime.AddMonths(i)
                        });
                    }
                }
            }
            else
            {
                eventRepetitions.Add(new EventRepetition
                {
                    Event = @event,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime
                });
            }

            @event.EventRepetitions = eventRepetitions;
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new {id = @event.Id}, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
                return NotFound();

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return Ok(@event);
        }

        // DELETE: api/Events/Repetition/5
        [HttpDelete("Repetition/{id}")]
        public async Task<IActionResult> DeleteRepetition([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var eventRepetition = await _context.EventRepetitions.SingleOrDefaultAsync(m => m.Id == id);
            if (eventRepetition == null)
                return NotFound();

            _context.EventRepetitions.Remove(eventRepetition);
            await _context.SaveChangesAsync();

            return Ok(eventRepetition);
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}