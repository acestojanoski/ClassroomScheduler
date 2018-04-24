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
        public int UserTypeId { get; set; }

        public string TypeName { get; set; }
    }
}
