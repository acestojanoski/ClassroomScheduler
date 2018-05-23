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
        [Route("LoggedUser")]
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
        [Route("UpdateUser")]
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
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserType = _context.UserTypes.Where(ut => ut.Id == model.UserTypeId).First();


            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Change password
        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return BadRequest("Could not find user!");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            if (changePasswordResult.Succeeded)
            {
                return Ok(changePasswordResult);
            }
            return BadRequest(changePasswordResult);
        }

        // POST: Delete user
        [HttpPost]
        [Authorize]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserViewModel model)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault().Value;
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return BadRequest("Could not find user!");
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest("Invalid password!");
            }            

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}