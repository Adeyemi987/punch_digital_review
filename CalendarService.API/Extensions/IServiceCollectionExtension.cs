using CalendarService.Core.Interfaces.Services.CoreServices;
using CalendarService.Core.Models.Constants;
using CalendarService.Core.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CalendarService.API.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void ResolveSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Calendar Service API",
                    Version = "v1",
                    Description = @"API service for creating, deleting, and updating events in a calendar",
                    Contact = new OpenApiContact
                    {
                        Name = "Adeyemi Tubosun",
                        Email = "cadeyemi50@gmail.com"
                    }
                });

                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                
            });
        }

        public static void ResolveAPICors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options => ConfigureCorsPolicy(options));

            CorsOptions ConfigureCorsPolicy(CorsOptions corsOptions)
            {
                string allowedHosts = config["Appsettings:AllowedHosts"];
                if (string.IsNullOrEmpty(allowedHosts))
                    corsOptions.AddPolicy("DenyAllHost",
                                      corsPolicyBuilder => corsPolicyBuilder
                                      .AllowAnyHeader()
                                      .WithMethods(new string[3] { "POST", "PATCH", "HEAD" })
                                     );
                else if (allowedHosts == "*")
                {
                    corsOptions.AddPolicy("AllowAll",
                                        corsPolicyBuilder => corsPolicyBuilder
                                        .AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        );
                }
                else
                {
                    string[] allowedHostArray;

                    if (!allowedHosts.Contains(","))
                        allowedHostArray = new string[1] { allowedHosts };
                    else
                        allowedHostArray = allowedHosts.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    corsOptions.AddPolicy("AllowAll",
                                      corsPolicyBuilder => corsPolicyBuilder
                                      .AllowAnyHeader()
                                      .WithOrigins(allowedHostArray)
                                      .WithMethods(new string[3] { "POST", "PATCH", "HEAD" })
                                     );
                }
                return corsOptions;
            }
        }

        public static void ResolveAppSettings(this IServiceCollection services, IConfiguration config)
        {
            AppSettings.EnableSwagger = config["AppSettings:EnableSwagger"];
            AppSettings.LogPath = config["AppSettings:LogPath"];

            if (string.IsNullOrEmpty(AppSettings.LogPath))
            {
                AppSettings.LogPath = AppContext.BaseDirectory;
            }
        }

        public static void ResolveCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IEventManagementService, EventManagementService>();
            
        }
    }
}
