using CalendarService.Infrastructure.Data.DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Test.Integration.Infrastructure
{
    public class DbContextProvideRelational
    {
        public static CalendarServiceDbContext InitContextWithInMemoryDbSupport()
        {
            var options = new DbContextOptionsBuilder<CalendarServiceDbContext>()
            .UseSqlite(CreateInMemoryDatabase()).Options;

            var _dbContext = new CalendarServiceDbContext(options);

            return _dbContext;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }



        public static CalendarServiceDbContext InitEmpty() => null;
    }
}
