using System;
namespace TaskManagement.Models.Enums
{
    public enum UserType
    {
        ADMIN = 1,
        MANAGER,
        USER
    }

    public static class UserTypeExtension
    {
        public static string? GetStringValue(this UserType userType)
        {
            return userType switch
            {
                UserType.USER => "user",
                UserType.ADMIN => "admin",
                UserType.MANAGER => "manager",
                _ => null
            };
        }
    }
}

