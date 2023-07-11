using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Data.Context;
using TaskManagement.Data.Implementations;
using TaskManagement.Data.Interfaces;
using TaskManagement.Services.Implementations;
using TaskManagement.Services.Interfaces;
using TaskManagement.Services.JWT;

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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IJWTAuthenticator, JwtAuthenticator>();
        }



        public static void JwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {

                string jwtConfig = configuration.GetSection("JwtConfig:Key").Value;
                string issuer = configuration.GetSection("JwtConfig:Issuer").Value;
                string audience = configuration.GetSection("JwtConfig:Audience").Value;
                var key = Encoding.ASCII.GetBytes(jwtConfig);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}