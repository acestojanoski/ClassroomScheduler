using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication7.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }
        public Course Course { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Repeat { get; set; }
        public int? RepeatTimes { get; set; }
        public ApplicationUser CreatedBy { get; set; }


    }
}
