using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Entities
{
    public class EventMembers : BaseEntity
    {
        public string Name { get; set; }
        public long EventId { get; set; }
        public Event Event { get; set; } 

    }
}
