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
        public int ClassRoomId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassRoomName { get; set; }

        [Required]
        public Building Building { get; set; }
    }
}
