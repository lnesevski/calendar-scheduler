using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarScheduler.Domain.DTO
{
    public class EventDTO
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public EventDateTimeString Start { get; set; }
        public EventDateTimeString End { get; set; }
    }

    public class EventDateTimeString
    {
        public string dateTime { get; set; }
        public string timeZone { get; set; }
    }
}
