using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassroomScheduler.Models;
using ClassroomScheduler.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Microsoft.AspNetCore.Authorization;

namespace ClassroomScheduler.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Courses")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Courses
        [HttpGet]
        public IEnumerable<Course> GetCourses()
        {
            return _context.Courses.Include(c => c.Professors).ThenInclude(p => p.Professor);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.Include(c => c.Professors).ThenInclude(p => p.Professor).SingleOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }


        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse([FromRoute] int id, [FromBody] CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = new Course
            {
                Id = id,
                Name = model.Name,
                Semester = model.Semester,
                Professors = new List<ProfessorCourse>()
            };

            List<ProfessorCourse> profCourses = new List<ProfessorCourse>();
            foreach (var forProf in model.ProfessorsId)
            {
                var tempProfessor = _userManager.Users.Include(c => c.Courses).FirstOrDefault(p => p.Id == forProf);
                ProfessorCourse pc = new ProfessorCourse
                {
                    Course = course,
                    Professor = tempProfessor
                };
                profCourses.Add(pc);
                course.Professors.Add(pc);

            }
            _context.ProfessorCourses.RemoveRange(_context.ProfessorCourses.Where(pc => pc.CourseId == id));
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            _context.ProfessorCourses.AddRange(profCourses);
            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        [HttpPost]
        public async Task<IActionResult> PostCourse([FromBody] CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = new Course
            {
                Name = model.Name,
                Semester = model.Semester,
                Professors = new List<ProfessorCourse>()
            };


            foreach (var forProf in model.ProfessorsId)
            {
                var tempProfessor = await _userManager.FindByIdAsync(forProf);
                course.Professors.Add(
                    new ProfessorCourse
                    {
                        Course = course,
                        Professor = tempProfessor
                    });
            }


            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}