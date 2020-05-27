using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using AirLineAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.OpenApi.Models;

namespace AirLineAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<AirLineContext>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IPassengerRepo, PassengerRepo>();
            services.AddScoped<ITimeTableRepository, TimeTableRepository>();
            services.AddScoped<IDestinationRepository, DestinationRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            });


            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "AirLineAPI", Version = "v1" }));


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirLineAPI v1");
            });

            app.UseMvc();
        }
    }
}
