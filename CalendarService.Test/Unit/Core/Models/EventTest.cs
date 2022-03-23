using CalendarService.Core.Entities;
using CalendarService.Test.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalendarService.Test.Unit.Core.Models
{
    public class EventTest
    {
        [Fact]
        public void Event_Should_Return_Set_Values() 
        { 
            var result = ModelsDataProvider.SetEvent();
            result.Id.ShouldBe(1);
            result.DateCreated.ShouldBe(DateTime.Today);
            result.EventMembers.ShouldBeOfType<List<EventMembers>>();
            result.EventOrganizer.ShouldBe("test");
            result.LastModifiedDate.ShouldBe(DateTime.Today);
            result.Location.ShouldBe("test");
            result.Name.ShouldBe("test");
            result.Time.ShouldBe(DateTime.Today);
        }
    }
}
