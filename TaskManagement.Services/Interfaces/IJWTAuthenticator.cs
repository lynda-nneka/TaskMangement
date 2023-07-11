using System;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using TaskManagement.Models.Entities;
using TaskManagement.Models.Dtos.Responses;

namespace TaskManagement.Services.Interfaces
{
    public interface IJWTAuthenticator
    {
        Task<JwtToken> GenerateJwtToken(ApplicationUser user, string expires = null, List<Claim> additionalClaims = null);
    }
}

