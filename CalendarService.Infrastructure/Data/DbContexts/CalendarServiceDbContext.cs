using CalendarService.Core.Entities;
using CalendarService.Core.Models.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CalendarService.Infrastructure.Data.DbContexts
{
    public class CalendarServiceDbContext : DbContext
    {
        public CalendarServiceDbContext(DbContextOptions<CalendarServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
        public DbSet<EventMembers> EventMembers { get; set; }
    }
}
