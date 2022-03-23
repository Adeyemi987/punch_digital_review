using CalendarService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Interfaces.Services.InfrastructureServices
{
    public interface ICalendarServiceQueries
    {
        Task<Event> GetEventWithTrackingAsync(long Id);
        Task<IEnumerable<EventMembers>> GetEventMembersWithTrackingAsync(long eventId);
        Task<IEnumerable<Event>> GetAllEventsWithNoTrackingAsync();
        Task<IEnumerable<Event>> GetAllEventsByOrganizerWithNoTrackingAsync(string eventOrganizer);
        Task<IEnumerable<Event>> GetAllEventsByLocationWithNoTrackingAsync(string location);
        Task<Event> GetEventByNameWithNoTrackingAsync(string name);

    }
}
