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
    [Route("api/ProfessorCourses")]
    public class ProfessorCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessorCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProfessorCourses
        [HttpGet]
        public IEnumerable<ProfessorCourse> GetProfessorCourses()
        {
            return _context.ProfessorCourses;
        }

        // GET: api/ProfessorCourses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfessorCourse([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var professorCourse = await _context.ProfessorCourses.SingleOrDefaultAsync(m => m.ProfessorId == id);

            if (professorCourse == null)
            {
                return NotFound();
            }

            return Ok(professorCourse);
        }

        // PUT: api/ProfessorCourses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessorCourse([FromRoute] string id, [FromBody] ProfessorCourse professorCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != professorCourse.ProfessorId)
            {
                return BadRequest();
            }

            _context.Entry(professorCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorCourseExists(id))
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

        // POST: api/ProfessorCourses
        [HttpPost]
        public async Task<IActionResult> PostProfessorCourse([FromBody] ProfessorCourse professorCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProfessorCourses.Add(professorCourse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfessorCourseExists(professorCourse.ProfessorId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProfessorCourse", new { id = professorCourse.ProfessorId }, professorCourse);
        }

        // DELETE: api/ProfessorCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessorCourse([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var professorCourse = await _context.ProfessorCourses.SingleOrDefaultAsync(m => m.ProfessorId == id);
            if (professorCourse == null)
            {
                return NotFound();
            }

            _context.ProfessorCourses.Remove(professorCourse);
            await _context.SaveChangesAsync();

            return Ok(professorCourse);
        }

        private bool ProfessorCourseExists(string id)
        {
            return _context.ProfessorCourses.Any(e => e.ProfessorId == id);
        }
    }
}