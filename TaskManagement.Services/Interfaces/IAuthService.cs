using System;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;

namespace TaskManagement.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AccountResponse> CreateUser(UserRegistrationRequest request);
        Task<AuthenticationResponse> UserLogin(LoginRequest request);
    }
}

