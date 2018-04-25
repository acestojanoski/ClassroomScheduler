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
    [Route("api/UserTypes")]
    public class UserTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserTypes
        [HttpGet]
        public IEnumerable<UserType> GetUserTypes()
        {
            return _context.UserTypes;
        }

        // GET: api/UserTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userType = await _context.UserTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (userType == null)
            {
                return NotFound();
            }

            return Ok(userType);
        }

        // PUT: api/UserTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType([FromRoute] int id, [FromBody] UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userType.Id)
            {
                return BadRequest();
            }

            _context.Entry(userType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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

        // POST: api/UserTypes
        [HttpPost]
        public async Task<IActionResult> PostUserType([FromBody] UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserType", new { id = userType.Id }, userType);
        }

        // DELETE: api/UserTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userType = await _context.UserTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.UserTypes.Remove(userType);
            await _context.SaveChangesAsync();

            return Ok(userType);
        }

        private bool UserTypeExists(int id)
        {
            return _context.UserTypes.Any(e => e.Id == id);
        }
    }
}