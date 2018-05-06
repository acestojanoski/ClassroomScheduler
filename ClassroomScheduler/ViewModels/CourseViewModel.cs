using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClassroomScheduler.Models;

namespace ClassroomScheduler.ViewModels
{
    public class CourseViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Semester { get; set; }

        public List<string> ProfessorsId { get; set; }
    }
}
