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
    public class CalendarServiceCommads : ICalendarServiceCommads
    {
        private readonly CalendarServiceDbContext _calendarServiceDbContext; 

        public CalendarServiceCommads(CalendarServiceDbContext doorAccessDbContext) 
        {
            _calendarServiceDbContext = doorAccessDbContext;
        }
        public async Task<long> AddEventAsync(Event evnt)
        {
            await _calendarServiceDbContext.Event.AddAsync(evnt);
            await _calendarServiceDbContext.SaveChangesAsync();
            return evnt.Id;
        }
        public async Task AddEventMembersAsync(List<EventMembers> eventMembers)
        {
            await _calendarServiceDbContext.EventMembers.AddRangeAsync(eventMembers);
            await _calendarServiceDbContext.SaveChangesAsync();
        }
        public async Task DeleteEventAsync(Event evnt)
        {
            _calendarServiceDbContext.Event.Remove(evnt);
            await _calendarServiceDbContext.SaveChangesAsync();
        }
        public async Task UpdateEventAsync(Event evnt)
        {
            _calendarServiceDbContext.Event.Update(evnt);
            await _calendarServiceDbContext.SaveChangesAsync();
        }
        public async Task DeleteEventMembersAsync(IEnumerable<EventMembers> eventMembers)
        {
            _calendarServiceDbContext.EventMembers.RemoveRange(eventMembers);
            await _calendarServiceDbContext.SaveChangesAsync();
        }

    }
}
