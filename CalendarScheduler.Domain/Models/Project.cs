using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarScheduler.Domain.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Title{ get; set; }
        public string Description { get; set; }
        [Description("How many hours to work on this project")]
        [Required]
        public long Hours { get; set; }
        [DisplayName("Work on Saturdays?")]
        public bool WorkOnSaturdays { get; set; }
        [DisplayName("Work on Sundays?")]
        public bool WorkOnSundays { get; set; }
        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime WorkStart { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public DateTime WorkEnd { get; set;  }
    }
}
