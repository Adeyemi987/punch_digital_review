using CalendarService.Core.Entities;
using CalendarService.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Interfaces.Services.CoreServices
{
    public interface IEventManagementService
    {
        Task<long> AddNewEventAsync(EventDTO newEvent);
        Task<bool> DeleteEventAsync(long eventId);
        Task<bool> EditEventAsync(EventDTO evntUpdate);
        Task<IEnumerable<EventWithTimeString>> GetAllEventsAsync();
        Task<IEnumerable<EventWithTimeString>> GetAllEventsByOrganizerAsync(string eventOrganizer);
        Task<EventWithTimeString> GetEventByIdAsync(long eventId);
        Task<IEnumerable<EventWithTimeString>> GetAllEventsByLocationAsync(string location); 
        Task<EventWithTimeString> GetEventByNameAsync(string name);
        Task<IEnumerable<EventWithTimeString>> GetAllEventsSortedByTimeAsync();
    }
}
