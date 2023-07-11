using System;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagement.Attributes
{
    public class AuthRequirement : IAuthorizationRequirement
    {
        private readonly string _routeName;
    }
}

