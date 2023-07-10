using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using TaskManagement.Data.Implementations;
using TaskManagement.Data.Interfaces;
using TaskManagement.Services.Implementations;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDBConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConn")
         ));
        }
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
            services.AddScoped<IServiceFactory, ServiceFactory>();
        }
    }
}