using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClassroomScheduler.Models;
using ClassroomScheduler.Services;
using ClassroomScheduler.ViewModels;
using ClassroomScheduler.ViewModes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ClassroomScheduler.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private ApplicationDbContext _context;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> 
            roleManager,IConfiguration configuration, IEmailSender emailSender, ILogger<AuthenticationController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                UserType = _context.UserTypes.Where(ut => ut.Id == model.UserTypeId).First()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Result = result } );
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("error", error.Description);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                                return BadRequest("Invalid login attempt.");

            }
            
            if (await _userManager.CheckPasswordAsync(user, vm.Password))
            {

                    JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                    return Ok(new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        Expiration = jwtSecurityToken.ValidTo,
                    });
                
            }

            return BadRequest("Invalid login attempt.");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();
            
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id),
                    new Claim("ip", ipAddress)
                }
                .Union(userClaims)
                .Union(roleClaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityToken:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JwtSecurityToken:Issuer"],
                audience: _configuration["JwtSecurityToken:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Could not continue with this request. (E1)");
            
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = _configuration.GetSection("ForgotPasswordSettings").GetSection("URL").Value;

            var callbackUrl = $"url";

            await _emailSender.SendEmailAsync(model.Email, "Reset Password",
              $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            return Ok(new { Message = $"Reset email has been sent, please check your inbox or spam folder and follow the link" });
        }

        [HttpPost]
        [Route("ResetPassword")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model,
            [FromQuery]string email, [FromQuery]string code)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            code = code.Replace(' ', '+');
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest("Could not continue with this request. (E1)");
            }
            var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<ApplicationUser> users = await _userManager.Users.Include(c => c.Courses).ThenInclude(cr => cr.Course).ToListAsync();
            return Ok(users);
        }

    }
}