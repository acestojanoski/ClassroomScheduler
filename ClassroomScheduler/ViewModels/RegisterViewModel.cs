using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClassroomScheduler.Models;

namespace ClassroomScheduler.ViewModes
{
    public class RegisterViewModel
    {
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(50, ErrorMessage = "The First name must be less than 50 characters long")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(50, ErrorMessage = "The Last name must be less than 50 characters long")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "User type")]
        public string UserType { get; set; }
    }
}
