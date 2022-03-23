using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarService.API.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigurSwagger(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseSwagger();

            appBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Calendar Service API");
                c.RoutePrefix = string.Empty;
            });

            return appBuilder;
        }

        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder appBuilder, IConfiguration config)
        {
            string allowedHosts = config["Appsettings:AllowedHosts"];

            if (string.IsNullOrEmpty(allowedHosts))
                return appBuilder.UseCors(x => x
                              .AllowAnyHeader()
                              .WithMethods(new string[3] { "POST", "PATCH", "HEAD" }));
            else
            {
                string[] allowedHostArray;

                if (!allowedHosts.Contains(","))
                    allowedHostArray = new string[1] { allowedHosts };
                else
                    allowedHostArray = allowedHosts.Split(",", StringSplitOptions.RemoveEmptyEntries);

                return appBuilder.UseCors(x => x
                          .AllowAnyHeader()
                          .WithOrigins(allowedHostArray)
                          .WithMethods(new string[3] { "POST", "PATCH", "HEAD" }));
            }
        }
    }
}
