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
        public string Title{ get; set; }
        public string Description { get; set; }
        [Description("How many hours to work on this project")]
        public long Hours { get; set; }
        [DisplayName("Work on Saturdays?")]
        public bool WorkOnSaturdays { get; set; }
        [DisplayName("Work on Sundays?")]
        public bool WorkOnSundays { get; set; }
        public DateTime Deadline { get; set; }
        [DisplayName("Work hours start")]
        [Range(0, 23, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public  int WorkStart{ get; set; }
        [DisplayName("Work hours end")]
        [Range(0, 23, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int WorkEnd { get; set;  }
    }
}
