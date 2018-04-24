using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomScheduler.Models
{
    public class UserType
    {
        [Key]
        [Required]
        public int UserTypeId { get; set; }

        [MaxLength(10)]
        [Required]
        public string TypeName { get; set; }
    }
}
