using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Models.DTOs
{
    public class EventWithTimeString : EventDTO
    {
        public new string Time { get; set; }
    }
}
