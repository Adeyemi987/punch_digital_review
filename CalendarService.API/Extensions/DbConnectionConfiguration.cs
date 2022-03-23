using CalendarService.Infrastructure.Data.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CalendarService.API.Extensions
{
    public static class DbConnectionConfiguration
    {
        private static string GetHerokuConnectionString()
        {
            // Get the Database URL from the ENV variables in Heroku
            string connectionURL = Environment.GetEnvironmentVariable("DATABASE_URL");

            // parse the connection string in to Universal Resource Identifier (URI)
            var databaseURI = new Uri(connectionURL);
            string database = databaseURI.LocalPath.TrimStart('/');
            string[] userInfo = databaseURI.UserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseURI.Host};Port={databaseURI.Port};" +
                   $"Database={database};Pooling=true;SSL Mode=Require;Trust Server Certificate=True";
        }


        public static void AddDbContextAndConfigurations(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            services.AddDbContextPool<CalendarServiceDbContext>(opt =>
            {
                string connectionStr;
                if (env.IsProduction())
                {
                    connectionStr = GetHerokuConnectionString();
                }
                else
                {
                    connectionStr = config.GetConnectionString("Default");
                }
                opt.UseNpgsql(connectionStr);
            });
        }
    }
}
