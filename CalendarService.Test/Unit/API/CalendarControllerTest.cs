using CalendarService.API.Controllers;
using CalendarService.Core.Interfaces.Services.CoreServices;
using CalendarService.Core.Models.DTOs;
using CalendarService.Test.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalendarService.Test.Unit.API
{
    public class CalendarControllerTest
    {
        CalendarController sut;
        Mock<IEventManagementService> mockIEventManagementService = new Mock<IEventManagementService>();

        public CalendarControllerTest() 
        {
            sut = new CalendarController(mockIEventManagementService.Object);
        }

        [Fact]
        public async Task AddEventAsync_Should_Return_201_After_Event_Created() 
        {
            //Arrange
            mockIEventManagementService.Setup(t => t.AddNewEventAsync(ModelsDataProvider.SetEventDTO())).ReturnsAsync(ModelsDataProvider.eventId);

            //Act
            var result = await sut.AddEventAsync(ModelsDataProvider.SetEventDTO());

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_404_If_Event_Not_Found() 
        {
            //Arrange
            mockIEventManagementService.Setup(t => t.DeleteEventAsync(ModelsDataProvider.eventId)).ReturnsAsync(false);

            //Act
            var result = await sut.DeleteEventAsync(ModelsDataProvider.eventId); 

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Return_204_If_Event_Deleted()
        {
            //Arrange
            mockIEventManagementService.Setup(t => t.DeleteEventAsync(ModelsDataProvider.eventId)).ReturnsAsync(true);

            //Act
            var result = await sut.DeleteEventAsync(ModelsDataProvider.eventId);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task EditEventAsync_Should_Return_404_If_Event_Not_Found()
        {
            //Arrange
            mockIEventManagementService.Setup(t => t.EditEventAsync(It.IsAny<EventDTO>())).ReturnsAsync(false);

            //Act
            var result = await sut.EditEventAsync(ModelsDataProvider.eventId, ModelsDataProvider.SetEventDTO());

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditEventAsync_Should_Return_400_If_Id_Does_Not_Macth_Payload()
        {
            //Arrange
            mockIEventManagementService.Setup(t => t.EditEventAsync(It.IsAny<EventDTO>())).ReturnsAsync(false);

            //Act
            var result = await sut.EditEventAsync(2, ModelsDataProvider.SetEventDTO());

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task EditEventAsync_Should_Return_204_If_Event_Edited()
        {
            //Arrange
            mockIEventManagementService.Setup(t => t.EditEventAsync(It.IsAny<EventDTO>())).ReturnsAsync(true);

            //Act
            var result = await sut.EditEventAsync(ModelsDataProvider.eventId, ModelsDataProvider.SetEventDTO());

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllEventsAsync_Should_Return_200_And_Data()
        {
            //Arrange
            List<EventWithTimeString> expected = new List<EventWithTimeString>
            {
                ModelsDataProvider.SetEventWithTimeString()
            };
            mockIEventManagementService.Setup(t => t.GetAllEventsAsync()).ReturnsAsync(expected);

            //Act
            var result = await sut.GetAllEventsAsync() as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task GetAllEventsByQueryStrings_Should_Return_200_And_Data_For_Organizer_Query_String()
        {
            //Arrange
            List<EventWithTimeString> expected = new List<EventWithTimeString>
            {
                ModelsDataProvider.SetEventWithTimeString()
            };
            mockIEventManagementService.Setup(t => t.GetAllEventsByOrganizerAsync("test")).ReturnsAsync(expected);

            //Act
            var result = await sut.GetAllEventsByQueryStrings("test",0,"","") as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task GetAllEventsByQueryStrings_Should_Return_200_And_Data_For_Id_Query_String()
        {
            //Arrange
            var expected = ModelsDataProvider.SetEventWithTimeString();
            mockIEventManagementService.Setup(t => t.GetEventByIdAsync(ModelsDataProvider.eventId)).ReturnsAsync(expected);

            //Act
            var result = await sut.GetAllEventsByQueryStrings("", 1, "", "") as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task GetAllEventsByQueryStrings_Should_Return_200_And_Data_For_Location_Query_String()
        {
            //Arrange
            List<EventWithTimeString> expected = new List<EventWithTimeString>
            {
                ModelsDataProvider.SetEventWithTimeString()
            };
            mockIEventManagementService.Setup(t => t.GetAllEventsByLocationAsync("test")).ReturnsAsync(expected);

            //Act
            var result = await sut.GetAllEventsByQueryStrings("", 0, "test", "") as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task GetAllEventsByQueryStrings_Should_Return_200_And_Data_For_Name_Query_String()
        {
            //Arrange
            var expected = ModelsDataProvider.SetEventWithTimeString();
            mockIEventManagementService.Setup(t => t.GetEventByNameAsync("test")).ReturnsAsync(expected);

            //Act
            var result = await sut.GetAllEventsByQueryStrings("", 0, "", "test") as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public async Task GetSortedEventsAsync_Should_Return_200_And_Data() 
        {
            //Arrange
            List<EventWithTimeString> expected = new List<EventWithTimeString>
            {
                ModelsDataProvider.SetEventWithTimeString()
            };
            mockIEventManagementService.Setup(t => t.GetAllEventsSortedByTimeAsync()).ReturnsAsync(expected);

            //Act
            var result = await sut.GetSortedEventsAsync() as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, result.Value);
        }
    }
}
