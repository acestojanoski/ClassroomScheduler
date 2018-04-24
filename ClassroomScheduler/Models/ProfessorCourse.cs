using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomScheduler.Models
{
    public class ProfessorCourse
    {
        public string ProfessorId { get; set; }
        public ApplicationUser Professor { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
