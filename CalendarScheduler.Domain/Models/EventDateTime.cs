using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarScheduler.Domain.Models
{
    public class EventDateTime
    {
        public DateTime date { get; set; }
        public DateTime dateTime { get; set; }
        public string TimeZone{ get; set; }
    }
}
