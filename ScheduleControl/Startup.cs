using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ScheduleControl.src.data;
using ScheduleControl.src.repositories;
using ScheduleControl.src.repositories.implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ScheduleControl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Database configuration
            if (Configuration["Enviroment:Start"] == "PROD")
            {
                services.AddEntityFrameworkNpgsql()
                .AddDbContext<ScheduleControlContext>(
                opt =>
                opt.UseNpgsql(Configuration["ConnectionStringsProd:DefaultConnection"]));
            }
            else
            {
                services.AddDbContext<ScheduleControlContext>(
                opt =>
                opt.UseSqlServer(Configuration["ConnectionStringsDev:DefaultConnection"]));
            }

            // Repositories configuration
            services.AddScoped<IDoctor, DoctorRepository>();
            services.AddScoped<IPatient, PatientRepository>();
            services.AddScoped<IAppointment, AppointmentRepository>();

            // Controllers configuration
            services.AddCors();
            services.AddControllers();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScheduleControl", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ScheduleControlContext context)
        {
            // Developer environment
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScheduleControl v1");
                    c.RoutePrefix = string.Empty;
                });
            }
            // Production environment
            context.Database.EnsureCreated();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScheduleControl v1");
                c.RoutePrefix = string.Empty;
            });

            // Routes
            app.UseRouting();
            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
