using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using TaskManagement.Attributes;
using TaskManagement.Data.Context;
using TaskManagement.Data.Seeds;
using TaskManagement.Extensions;
using TaskManagement.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterServices();

builder.Services.AddDBConnection(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.JwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("Authorization", policy => policy.Requirements.Add(new AuthRequirement()));
});



builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VOTING PORTAL", Version = "v1" });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                            Array.Empty<string>()
                    },
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.SeedData();

app.MapControllers();

app.Run();

