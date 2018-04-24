using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomScheduler.Models
{
    public class Course
    {
        [Key]
        [Required]
        public int CourseId { get; set; }

        [MaxLength(100)]
        [Required]
        public string CourseName { get; set; }

        public int Semester { get; set; }
        public List<ProfessorCourse> ProfessorCourses { get; set; }

    }
}
