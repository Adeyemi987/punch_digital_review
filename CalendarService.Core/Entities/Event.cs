using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Entities
{
    public class Event : BaseEntity
    {
        public Event()
        {
            EventMembers = new List<EventMembers>();
        }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public string Location { get; set; }
        public string EventOrganizer { get; set; }
        public ICollection<EventMembers> EventMembers { get; set; } 
    }
}
