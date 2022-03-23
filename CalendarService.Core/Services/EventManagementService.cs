using CalendarService.Core.Entities;
using CalendarService.Core.Interfaces.Helpers;
using CalendarService.Core.Interfaces.Services.CoreServices;
using CalendarService.Core.Interfaces.Services.InfrastructureServices;
using CalendarService.Core.Models.Constants;
using CalendarService.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CalendarService.Core.Services
{
    public class EventManagementService : IEventManagementService
    {
        private readonly ICalendarServiceCommads _calendarServiceCommads;
        private readonly ICalendarServiceQueries _calendarServiceQueries;
        public EventManagementService(ICalendarServiceCommads calendarServiceCommads, ICalendarServiceQueries calendarServiceQueries) 
        {
            _calendarServiceCommads = calendarServiceCommads;
            _calendarServiceQueries = calendarServiceQueries;
        }
        public async Task<long> AddNewEventAsync(EventDTO newEvent)
        {
            Event evnt = new Event();
            long eventId;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(newEvent.Time);

                evnt.Name = newEvent.Name;
                evnt.EventOrganizer = newEvent.Organizer;
                evnt.Location = newEvent.Location;
                evnt.Time = dateTimeOffset.UtcDateTime.AddHours(1);
                evnt.LastModifiedDate = DateTime.Now;
                evnt.DateCreated = DateTime.Now;

                eventId = await _calendarServiceCommads.AddEventAsync(evnt);

                List<EventMembers> eventMembers = new();
                foreach (string name in newEvent.Members.Split(','))
                {
                    EventMembers eventMember = new()
                    {
                        Name = name,
                        EventId = eventId,
                        DateCreated = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    };
                    eventMembers.Add(eventMember);
                }
                await _calendarServiceCommads.AddEventMembersAsync(eventMembers);
                scope.Complete();
            }
            return eventId;
        }
        public async Task<bool> DeleteEventAsync(long eventId)
        {
            var evnt = await _calendarServiceQueries.GetEventWithTrackingAsync(eventId);
            if(evnt == null)
            {
                return false;
            }
            await _calendarServiceCommads.DeleteEventAsync(evnt);
            return true;
        }
        public async Task<bool> EditEventAsync(EventDTO evntUpdate)
        {
            var evnt = await _calendarServiceQueries.GetEventWithTrackingAsync(evntUpdate.Id);
            if (evnt == null)
            {
                return false;
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(evntUpdate.Time);
                evnt.Name = evntUpdate.Name;
                evnt.EventOrganizer = evntUpdate.Organizer;
                evnt.Location = evntUpdate.Location;
                evnt.Time = dateTimeOffset.UtcDateTime.AddHours(1);
                evnt.LastModifiedDate = DateTime.Now;
                evnt.DateCreated = DateTime.Now;

                var evntMembers = await _calendarServiceQueries.GetEventMembersWithTrackingAsync(evnt.Id);
                await _calendarServiceCommads.DeleteEventMembersAsync(evntMembers);
                await _calendarServiceCommads.UpdateEventAsync(evnt);

                List<EventMembers> eventMembers = new();
                foreach (string name in evntUpdate.Members.Split(','))
                {
                    EventMembers eventMember = new()
                    {
                        Name = name,
                        EventId = evnt.Id,
                        DateCreated = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    };
                    eventMembers.Add(eventMember);
                }
                await _calendarServiceCommads.AddEventMembersAsync(eventMembers);
                scope.Complete();
            }    
            return true;
        }
        public async Task<IEnumerable<EventWithTimeString>> GetAllEventsAsync()
        {
            var allEvents = await _calendarServiceQueries.GetAllEventsWithNoTrackingAsync();
            List<EventWithTimeString> EventsList = new List<EventWithTimeString>();
            foreach(var evnt in allEvents)
            {
                EventsList.Add(MapEventDetails(evnt));
            }
            return EventsList;
        }
        public async Task<IEnumerable<EventWithTimeString>> GetAllEventsByOrganizerAsync(string eventOrganizer)
        {
            var allEvents = await _calendarServiceQueries.GetAllEventsByOrganizerWithNoTrackingAsync(eventOrganizer);
            List<EventWithTimeString> EventsList = new List<EventWithTimeString>();
            foreach (var evnt in allEvents)
            {
                EventsList.Add(MapEventDetails(evnt));
            }
            return EventsList;
        }
        public async Task<EventWithTimeString> GetEventByIdAsync(long eventId)   
        {
            var evnt =  await _calendarServiceQueries.GetEventWithTrackingAsync(eventId);
            return evnt == null ? null : MapEventDetails(evnt);
        }
        public async Task<IEnumerable<EventWithTimeString>> GetAllEventsByLocationAsync(string location)
        {
            var allEvents = await _calendarServiceQueries.GetAllEventsByLocationWithNoTrackingAsync(location);
            List<EventWithTimeString> EventsList = new List<EventWithTimeString>();
            foreach (var evnt in allEvents)
            {
                EventsList.Add(MapEventDetails(evnt));
            }
            return EventsList;
        }
        public async Task<EventWithTimeString> GetEventByNameAsync(string name)
        {
            var evnt = await _calendarServiceQueries.GetEventByNameWithNoTrackingAsync(name);
            return evnt == null ? null : MapEventDetails(evnt);
        }
        public async Task<IEnumerable<EventWithTimeString>> GetAllEventsSortedByTimeAsync()  
        {
            var allEvents = await _calendarServiceQueries.GetAllEventsWithNoTrackingAsync();
            allEvents = allEvents.OrderByDescending(x => x.Time).ToList();
            List<EventWithTimeString> EventsList = new();
            foreach (var evnt in allEvents)
            {
                EventsList.Add(MapEventDetails(evnt));            
            }
            return EventsList;
        }
        private EventWithTimeString MapEventDetails(Event evnt)
        {
            EventWithTimeString eventDTO = new()
            {
                Id = evnt.Id,
                Name = evnt.Name,
                Location = evnt.Location,
                Organizer = evnt.EventOrganizer,
                Time = evnt.Time.ToString("dd-MM-yyyy HH:mm:ss"),
                Members = string.Join(",", evnt.EventMembers.Select(x => x.Name))
            };
            return eventDTO;
        }
    }
}
