using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class EventType
    {
        [Key]
        public int EventTypeId { get; set; }

        public string EventTypeName { get; set; }

    }
}
