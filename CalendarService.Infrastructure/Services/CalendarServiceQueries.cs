using CalendarService.Core.Entities;
using CalendarService.Core.Interfaces.Services.InfrastructureServices;
using CalendarService.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Infrastructure.Services
{
    public class CalendarServiceQueries : ICalendarServiceQueries
    {
        private readonly CalendarServiceDbContext _calendarServiceDbContext;

        public CalendarServiceQueries(CalendarServiceDbContext doorAccessDbContext) 
        {
            _calendarServiceDbContext = doorAccessDbContext;
        }
        public async Task<Event> GetEventWithTrackingAsync(long Id)
        {
            return await _calendarServiceDbContext.Event.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<IEnumerable<EventMembers>> GetEventMembersWithTrackingAsync(long eventId)
        {
            return await _calendarServiceDbContext.EventMembers.Where(x => x.EventId == eventId).ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetAllEventsWithNoTrackingAsync()
        {
            return await _calendarServiceDbContext.Event.Include(x => x.EventMembers).ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetAllEventsByOrganizerWithNoTrackingAsync(string eventOrganizer)
        {
            return await _calendarServiceDbContext.Event.AsNoTracking().Include(x => x.EventMembers)
                .Where(x => x.EventOrganizer == eventOrganizer).ToListAsync();
        }
        public async Task<IEnumerable<Event>> GetAllEventsByLocationWithNoTrackingAsync(string location)
        {
            return await _calendarServiceDbContext.Event.AsNoTracking().Include(x => x.EventMembers)
                .Where(x => x.Location == location).ToListAsync();
        }
        public async Task<Event> GetEventByNameWithNoTrackingAsync(string name)
        {
            return await _calendarServiceDbContext.Event.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
        }

    }
}
