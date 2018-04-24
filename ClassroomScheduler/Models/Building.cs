using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomScheduler.Models
{
    public class Building
    {
        [Key]
        [Required]
        public int BuildingId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BuildingName { get; set; }
    }
}
