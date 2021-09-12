using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarScheduler.Domain.Models
{
    public class Event
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }

        public Event(string TimeZone)
        {
            this.Start = new EventDateTime()
            {
                TimeZone = TimeZone ?? TimeZoneInfo.Local.StandardName
            };

            this.End = new EventDateTime() 
            {
                TimeZone = TimeZone ?? TimeZoneInfo.Local.StandardName
            };
        }
    }
}
