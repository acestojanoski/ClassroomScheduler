using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomScheduler.Models
{
    public class ClassRoom
    {
        [Key]
        [Required]
        public int ClassRoomId { get; set; }

        [MaxLength(50)]
        [Required]
        public string ClassRoomName { get; set; }

        [Required]
        public Building Building { get; set; }
    }
}
