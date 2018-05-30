using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomScheduler.ViewModels
{
    public class EventEditViewModel
    {
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start time")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End time")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Required]
        public int EventTypeId { get; set; }

        [Required]
        public int ClassRoomId { get; set; }

        public int? CourseId { get; set; }
    }
}
