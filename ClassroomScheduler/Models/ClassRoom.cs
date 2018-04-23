using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class ClassRoom
    {
        [Key]
        public int ClassRoomId { get; set; }

        public string ClassRoomName { get; set; }
        public Building Building { get; set; }
    }
}
