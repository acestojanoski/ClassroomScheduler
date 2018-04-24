using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ClassroomScheduler.Models
{
    public class ApplicationUser : IdentityUser
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public List<ProfessorCourse> Courses { get; set; }
    }
}
