using CalendarService.Core.Entities;
using CalendarService.Core.Interfaces.Services.CoreServices;
using CalendarService.Core.Interfaces.Services.InfrastructureServices;
using CalendarService.Core.Models.DTOs;
using CalendarService.Core.Services;
using CalendarService.Test.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalendarService.Test.Unit.Core.Services
{
    public class EventManagementServiceTest
    {
        IEventManagementService sut;
        Mock<ICalendarServiceCommads> _mockICalendarServiceCommads = new Mock<ICalendarServiceCommads>();
        Mock<ICalendarServiceQueries> _mockICalendarServiceQueries = new Mock<ICalendarServiceQueries>();

        public EventManagementServiceTest()
        {
            sut = new EventManagementService(_mockICalendarServiceCommads.Object, _mockICalendarServiceQueries.Object);
        }

        [Fact]
        public async Task AddNewEventAsync_Should_Add_New_Event_And_Return_Eveny_Id()
        {
            //Arrange
            _mockICalendarServiceCommads.Setup(t => t.AddEventAsync(It.IsAny<Event>())).ReturnsAsync(1);
            //Act
            var result = await sut.AddNewEventAsync(ModelsDataProvider.SetEventDTO());
            //Assert
            Assert.Equal(ModelsDataProvider.eventId, result);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_False_When_Event_Not_Exist()
        {
            //Arrange
            Event evnt = null;
            _mockICalendarServiceQueries.Setup(t => t.GetEventWithTrackingAsync(It.IsAny<long>())).ReturnsAsync(evnt);
            //Act
            var result = await sut.DeleteEventAsync(ModelsDataProvider.eventId);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_true_When_Event_Is_Deleted()
        {
            //Arrange
            _mockICalendarServiceQueries.Setup(t => t.GetEventWithTrackingAsync(It.IsAny<long>())).ReturnsAsync(ModelsDataProvider.SetEvent());
            //Act
            var result = await sut.DeleteEventAsync(ModelsDataProvider.eventId);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task EditEventAsync_Should_Return_False_When_Event_Not_Exist()
        {
            //Arrange
            Event evnt = null;
            _mockICalendarServiceQueries.Setup(t => t.GetEventWithTrackingAsync(It.IsAny<long>())).ReturnsAsync(evnt);
            //Act
            var result = await sut.EditEventAsync(ModelsDataProvider.SetEventDTO());
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task EditEventAsync_Should_Return_true_When_Event_Is_Edited() 
        {
            //Arrange
            _mockICalendarServiceQueries.Setup(t => t.GetEventWithTrackingAsync(It.IsAny<long>())).ReturnsAsync(ModelsDataProvider.SetEvent());
            //Act
            var result = await sut.EditEventAsync(ModelsDataProvider.SetEventDTO());
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllEventsAsync_Should_Return_List_Of_Events() 
        {
            //Arrange
            _mockICalendarServiceQueries.Setup(t => t.GetAllEventsWithNoTrackingAsync()).ReturnsAsync(new List<Event>() { ModelsDataProvider.SetEvent()});
            //Act
            var result = await sut.GetAllEventsAsync();
            //Assert
            Assert.IsType<List<EventWithTimeString>>(result);
        }

        [Fact]
        public async Task GetAllEventsByOrganizerAsync_Should_Return_List_Of_Events()
        {
            //Arrange
            _mockICalendarServiceQueries.Setup(t => t.GetAllEventsByOrganizerWithNoTrackingAsync(It.IsAny<string>())).ReturnsAsync(new List<Event>() { ModelsDataProvider.SetEvent() });
            //Act
            var result = await sut.GetAllEventsByOrganizerAsync("test");
            //Assert
            Assert.IsType<List<EventWithTimeString>>(result);
        }

        [Fact]
        public async Task GetEventByIdAsync_Should_Return_Null_If_Event_Not_Exists()
        {
            //Arrange
            Event evnt = null;
            _mockICalendarServiceQueries.Setup(t => t.GetEventWithTrackingAsync(It.IsAny<long>())).ReturnsAsync(evnt);
            //Act
            var result = await sut.GetEventByIdAsync(ModelsDataProvider.eventId);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEventByIdAsync_Should_Return_Event()
        {
            //Arrange
            _mockICalendarServiceQueries.Setup(t => t.GetEventWithTrackingAsync(It.IsAny<long>())).ReturnsAsync(ModelsDataProvider.SetEvent());
            //Act
            var result = await sut.GetEventByIdAsync(ModelsDataProvider.eventId);
            //Assert
            Assert.IsType<EventWithTimeString>(result);
        }


        [Fact]
        public async Task GetAllEventsByLocationAsync_Should_Return_List_Of_Events()
        {
            //Arrange
            _mockICalendarServiceQueries.Setup(t => t.GetAllEventsByLocationWithNoTrackingAsync(It.IsAny<string>())).ReturnsAsync(new List<Event>() { ModelsDataProvider.SetEvent() });
            //Act
            var result = await sut.GetAllEventsByLocationAsync("test");
            //Assert
            Assert.IsType<List<EventWithTimeString>>(result);
        }

        [Fact]
        public async Task GetEventByNameAsync_Should_Return_Null_If_Event_Not_Exists()
        {
            //Arrange
            Event evnt = null;
            _mockICalendarServiceQueries.Setup(t => t.GetEventByNameWithNoTrackingAsync(It.IsAny<string>())).ReturnsAsync(evnt);
            //Act
            var result = await sut.GetEventByNameAsync("test");
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEventByNameAsync_Should_Return_Event()
        {
            //Arrange
            Event evnt = null;
            _mockICalendarServiceQueries.Setup(t => t.GetEventByNameWithNoTrackingAsync(It.IsAny<string>())).ReturnsAsync(ModelsDataProvider.SetEvent());
            //Act
            var result = await sut.GetEventByNameAsync("test");
            //Assert
            Assert.IsType<EventWithTimeString>(result);
        }

        [Fact]
        public async Task GetAllEventsSortedByTimeAsync_Should_Return_List_Of_Events()
        {
            //Arrange
            _mockICalendarServiceQueries.Setup(t => t.GetAllEventsWithNoTrackingAsync()).ReturnsAsync(new List<Event>() { ModelsDataProvider.SetEvent() });
            //Act
            var result = await sut.GetAllEventsSortedByTimeAsync();
            //Assert
            Assert.IsType<List<EventWithTimeString>>(result);
        }

    }
}
