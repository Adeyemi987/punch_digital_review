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
    public class EventWithTimeString
    {
        [Fact]
        public void EventDTO_Should_Return_Set_Values()
        {
            var result = ModelsDataProvider.SetEventWithTimeString();
            result.Id.ShouldBe(1);
            result.Location.ShouldBe("test");
            result.Members.ShouldBe("test");
            result.Name.ShouldBe("test");
            result.Organizer.ShouldBe("test");
            result.Time.ShouldBe("test");
        }
    }
}
