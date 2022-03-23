using CalendarService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Interfaces.Services.InfrastructureServices
{
    public interface ICalendarServiceCommads
    {
        Task<long> AddEventAsync(Event evnt); 
        Task AddEventMembersAsync(List<EventMembers> eventMembers);
        Task DeleteEventAsync(Event evnt);
        Task UpdateEventAsync(Event evnt);
        Task DeleteEventMembersAsync(IEnumerable<EventMembers> eventMembers);
    }
}
