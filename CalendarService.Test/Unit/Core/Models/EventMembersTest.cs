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
    public class EventMembersTest
    {
        [Fact]
        public void EventMembers_Should_Return_Set_Values()
        {
            var result = ModelsDataProvider.SetEventMembers();
            result.Id.ShouldBe(1);
            result.DateCreated.ShouldBe(DateTime.Today);
            result.Name.ShouldBe("test");
            result.Event.ShouldBeOfType<Event>();
            result.LastModifiedDate.ShouldBe(DateTime.Today);
            result.EventId.ShouldBe(1);
        }
    }
}
