using CalendarService.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Test.Integration.Infrastructure
{
    public class DbContextProvider
    {
        public static CalendarServiceDbContext InitContextWithInMemoryDbSupport()
        {
            var options = new DbContextOptionsBuilder<CalendarServiceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var _dbContext = new CalendarServiceDbContext(options);
            _dbContext.Database.EnsureCreated();

            return _dbContext;
        }

        public static CalendarServiceDbContext InitEmpty() => null;
    }
}
