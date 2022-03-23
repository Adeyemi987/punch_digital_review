using CalendarService.Core.Interfaces.Helpers;
using CalendarService.Core.Interfaces.Services.InfrastructureServices;
using CalendarService.Core.Models.Constants;
using CalendarService.Infrastructure.Data.DbContexts;
using CalendarService.Infrastructure.Helpers;
using CalendarService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CalendarService.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ResolveInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<ICalendarServiceCommads, CalendarServiceCommads>();
            services.AddScoped<ICalendarServiceQueries, CalendarServiceQueries>();
        }
    }
}
