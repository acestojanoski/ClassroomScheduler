using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClassroomScheduler.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public bool Repeat { get; set; }

        public int? RepeatTimes { get; set; }

        [Required]
        public EventType EventType { get; set; }

        public Course Course { get; set; }

        [Required]
        public ClassRoom ClassRoom { get; set; }

        public string Description { get; set; }

        [Required]
        public ApplicationUser CreatedBy { get; set; }
    }
}
