using System;
using TaskManagement.Data.Context;
using TaskManagement.Models.Entities;
using TaskManagement.Models.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagement.Data.Seeds
{
    public static class DatabaseDataSeeder
    {

        public static void SeedData(this IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();
            var roleExist = context.Roles.Any();

            if (!roleExist)
            {
                context.Roles.AddRange(SeededRoles());
                context.SaveChanges();
            }
        }
         private static IEnumerable<ApplicationRole> SeededRoles()
         {
            return new List<ApplicationRole>()
            {
                new ApplicationRole()
                {
                    Name = UserTypeExtension.GetStringValue(UserType.USER),
                    Type = UserType.USER,
                    NormalizedName = UserTypeExtension.GetStringValue(UserType.USER)?.ToUpper()
                                                      .Normalize()
                },
                new ApplicationRole()
                {
                    Name = UserTypeExtension.GetStringValue(UserType.ADMIN),
                    Type = UserType.ADMIN,
                    NormalizedName = UserTypeExtension.GetStringValue(UserType.ADMIN)?.ToUpper()
                                                      .Normalize()
                },
                new ApplicationRole
                {
                    Name = UserTypeExtension.GetStringValue(UserType.MANAGER),
                    Type = UserType.MANAGER,
                    NormalizedName = UserTypeExtension.GetStringValue(UserType.MANAGER)?.ToUpper()
                                                      .Normalize()
                }
            };
        }
    }
}

