using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassroomScheduler.Models;
using ClassroomScheduler.ViewModels;

namespace ClassroomScheduler.Controllers
{
    [Produces("application/json")]
    [Route("api/ClassRooms")]
    public class ClassRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClassRooms
        [HttpGet]
        public IEnumerable<ClassRoom> GetClassRooms()
        {
            return _context.ClassRooms.Include(b => b.Building);
        }

        // GET: api/ClassRooms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassRoom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classRoom = await _context.ClassRooms.Include(b => b.Building).SingleOrDefaultAsync(m => m.Id == id);

            if (classRoom == null)
            {
                return NotFound();
            }

            return Ok(classRoom);
        }

        // PUT: api/ClassRooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassRoom([FromRoute] int id, [FromBody] ClassRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classRoom = new ClassRoom
            {
                Id = id,
                Name = model.Name,
                Building = _context.Buildings.Where(b => b.Id == model.BuildingId).First()
            };

            _context.Entry(classRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassRoomExists(id))
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

        // POST: api/ClassRooms
        [HttpPost]
        public async Task<IActionResult> PostClassRoom([FromBody] ClassRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classRoom = new ClassRoom
            {
                Name = model.Name,
                Building = _context.Buildings.Where(b => b.Id == model.BuildingId).First()
            };

            _context.ClassRooms.Add(classRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassRoom", new { id = classRoom.Id }, classRoom);
        }

        // DELETE: api/ClassRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassRoom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classRoom = await _context.ClassRooms.SingleOrDefaultAsync(m => m.Id == id);
            if (classRoom == null)
            {
                return NotFound();
            }

            _context.ClassRooms.Remove(classRoom);
            await _context.SaveChangesAsync();

            return Ok(classRoom);
        }

        private bool ClassRoomExists(int id)
        {
            return _context.ClassRooms.Any(e => e.Id == id);
        }
    }
}