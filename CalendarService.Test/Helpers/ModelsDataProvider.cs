using CalendarService.Core.Entities;
using CalendarService.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Test.Helpers
{
    public static class ModelsDataProvider
    {
        public const long eventId = 1;
        public static BaseEntity GetBaseEntity()
        {
            return new()
            {
                Id = 1,
                DateCreated = DateTime.Today,
                LastModifiedDate = DateTime.Today
            };
        }
        public static Event SetEvent()
        {
            return new()
            {
                DateCreated = DateTime.Today,
                EventMembers = new List<EventMembers>(),
                EventOrganizer = "test",
                Id = 1,
                LastModifiedDate = DateTime.Today,
                Location = "test",
                Name = "test",
                Time = DateTime.Today
            };
        }
        public static EventMembers SetEventMembers()
        {
            return new()
            {
                DateCreated = DateTime.Today,
                Event = SetEvent(),
                EventId = 1,
                Id = 1,
                LastModifiedDate = DateTime.Today,
                Name = "test"
            };
        }
        public static EventDTO SetEventDTO()
        {
            return new()
            {
                Id = 1,
                Location = "test",
                Members = "test",
                Name = "test",
                Organizer = "test",
                Time = 1
            };
        }
        public static EventWithTimeString SetEventWithTimeString()
        {
            return new()
            {
                Id = 1,
                Location = "test",
                Members = "test",
                Name = "test",
                Organizer = "test",
                Time = "test"
            };
        }
    }
}
