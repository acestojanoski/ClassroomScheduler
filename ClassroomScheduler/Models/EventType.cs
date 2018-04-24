using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomScheduler.Models
{
    public class EventType
    {
        [Key]
        [Required]
        public int EventTypeId { get; set; }

        [MaxLength(50)]
        [Required]
        public string EventTypeName { get; set; }

    }
}
