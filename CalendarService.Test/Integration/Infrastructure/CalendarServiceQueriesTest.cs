using CalendarService.Core.Entities;
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
    public class CalendarServiceQueriesTest
    {
        CalendarServiceDbContext dbContext = DbContextProvider.InitContextWithInMemoryDbSupport();
        CalendarServiceQueries sut;
        public CalendarServiceQueriesTest() 
        {

        }

        [Fact]
        public async Task GetEventWithTrackingAsync_Should_Get_Saved_Data_From_Db()
        {
            //Arrange
            dbContext.Event.Add(ModelsDataProvider.SetEvent());
            dbContext.SaveChanges();

            //Act
            sut = new CalendarServiceQueries(dbContext);
            var result = await sut.GetEventWithTrackingAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Event>(result);
        }

        [Fact]
        public async Task GetEventMembersWithTrackingAsync_Should_Get_Saved_Data_From_Db()
        {
            //Arrange
            dbContext.EventMembers.Add(ModelsDataProvider.SetEventMembers());
            dbContext.SaveChanges();

            //Act
            sut = new CalendarServiceQueries(dbContext);
            var result = await sut.GetEventMembersWithTrackingAsync(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<EventMembers>>(result);
        }

        [Fact]
        public async Task GetAllEventsWithNoTrackingAsync_Should_Get_Saved_Data_From_Db()
        {
            //Arrange
            dbContext.Event.Add(ModelsDataProvider.SetEvent());
            dbContext.SaveChanges();

            //Act
            sut = new CalendarServiceQueries(dbContext);
            var result = await sut.GetAllEventsWithNoTrackingAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Event>>(result);
        }

        [Fact]
        public async Task GetAllEventsByOrganizerWithNoTrackingAsync_Should_Get_Saved_Data_From_Db()
        {
            //Arrange
            dbContext.Event.Add(ModelsDataProvider.SetEvent());
            dbContext.SaveChanges();

            //Act
            sut = new CalendarServiceQueries(dbContext);
            var result = await sut.GetAllEventsByOrganizerWithNoTrackingAsync("test");

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Event>>(result);
        }


        [Fact]
        public async Task GetAllEventsByLocationWithNoTrackingAsync_Should_Get_Saved_Data_From_Db()
        {
            //Arrange
            dbContext.Event.Add(ModelsDataProvider.SetEvent());
            dbContext.SaveChanges();

            //Act
            sut = new CalendarServiceQueries(dbContext);
            var result = await sut.GetAllEventsByLocationWithNoTrackingAsync("test");

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Event>>(result);
        }

        [Fact]
        public async Task GetEventByNameWithNoTrackingAsync_Should_Get_Saved_Data_From_Db()
        {
            //Arrange
            dbContext.Event.Add(ModelsDataProvider.SetEvent());
            dbContext.SaveChanges();

            //Act
            sut = new CalendarServiceQueries(dbContext);
            var result = await sut.GetEventByNameWithNoTrackingAsync("test");

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Event>(result);
        }
    }
}
