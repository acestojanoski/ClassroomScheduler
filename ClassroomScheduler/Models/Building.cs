using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class Building
    {
        [Key]
        public int Id { get; set; }

        public string BuildingName { get; set; }
    }
}
