using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassroomScheduler.Models;
using ClassroomScheduler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClassroomScheduler.Controllers
{
    [Produces("application/json")]
    [Route("api/ManageUser")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public ManageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Logged User
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LoggedUser()
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        // PUT: Update user
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return BadRequest("Could not find user!");

            user.Email = model.Email;
            user.UserName = model.UserName;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // DELETE: Delete user
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;

            if (!String.IsNullOrEmpty(userName))
                return BadRequest("Empty parameter!");

            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return BadRequest("Could not find user!");

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}