using CalendarService.API.Extensions;
using CalendarService.API.Middleware;
using CalendarService.Core.Models.Constants;
using CalendarService.Infrastructure.Data.DbContexts;
using CalendarService.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ResolveAPICors(Configuration);
            services.ResolveCoreServices();
            services.ResolveAppSettings(Configuration);
            services.ResolveInfrastructureServices();
            if (AppSettings.EnableSwagger == "true")
            {
                services.ResolveSwagger();
            }
            services.AddControllers();
            services.AddDbContextAndConfigurations(Environment, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            if (AppSettings.EnableSwagger == "true")
            {
                app.ConfigurSwagger();
            }
            app.UseRouting();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.ConfigureCors(Configuration);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
