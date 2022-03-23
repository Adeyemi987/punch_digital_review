using CalendarService.Core.Entities;
using CalendarService.Core.Interfaces.Services.InfrastructureServices;
using CalendarService.Infrastructure.Data.DbContexts;
using CalendarService.Infrastructure.Services;
using CalendarService.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalendarService.Test.Integration.Infrastructure
{
    public class CalendarServiceCommadsTest
    {
        CalendarServiceDbContext dbContext = DbContextProvider.InitContextWithInMemoryDbSupport();
        ICalendarServiceCommads sut;
        public CalendarServiceCommadsTest() 
        {

        }

        [Fact]
        public async Task AddEventAsync_Should_Add_New_Event_To_DB() 
        {
            //Arrange
            sut = new CalendarServiceCommads(dbContext);
            var newEvent = ModelsDataProvider.SetEvent();

            //Act
            await sut.AddEventAsync(newEvent);
            var result = dbContext.Event.Where(o => o.Id == ModelsDataProvider.SetEvent().Id).FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Event>(result);
        }

        [Fact]
        public async Task AddEventMembersAsync_Should_Add_New_Event_Members_To_DB()
        {
            //Arrange
            sut = new CalendarServiceCommads(dbContext); 
            List<EventMembers> newMembers = new List<EventMembers>
            {
                ModelsDataProvider.SetEventMembers()
            };

            //Act
            await sut.AddEventMembersAsync(newMembers);
            var result = dbContext.EventMembers.Where(o => o.Id == ModelsDataProvider.SetEventMembers().Id).FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<EventMembers>(result);
        }

        [Fact]
        public async Task DeleteEventAsync_Should_Remove_Event_From_DB()
        {
            //Arrange
            sut = new CalendarServiceCommads(dbContext);
            var newEvent = ModelsDataProvider.SetEvent();

            //Act         
            await sut.AddEventAsync(newEvent);
            await sut.DeleteEventAsync(newEvent);
            var result = dbContext.Event.Where(o => o.Id == ModelsDataProvider.SetEvent().Id).FirstOrDefault();

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateEventAsync_Should_Update_Event_In_DB() 
        {
            //Arrange
            sut = new CalendarServiceCommads(dbContext);
            var newEvent = ModelsDataProvider.SetEvent();

            //Act
            await sut.AddEventAsync(newEvent);
            newEvent.Location = "Alabama";
            await sut.UpdateEventAsync(newEvent);
            var result = dbContext.Event.Where(o => o.Id == ModelsDataProvider.SetEvent().Id).FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Event>(result);
            Assert.Equal("Alabama", result.Location);
        }

        [Fact]
        public async Task DeleteEventMembersAsync_Should_Remove_Event_Members_From_DB() 
        {
            //Arrange
            sut = new CalendarServiceCommads(dbContext);
            List<EventMembers> Members = new List<EventMembers>
            {
                ModelsDataProvider.SetEventMembers()
            };

            //Act
            await sut.AddEventMembersAsync(Members);
            await sut.DeleteEventMembersAsync(Members);
            var result = dbContext.EventMembers.Where(o => o.EventId == ModelsDataProvider.SetEvent().Id).FirstOrDefault();

            //Assert
            Assert.Null(result);
        }
    }
}
