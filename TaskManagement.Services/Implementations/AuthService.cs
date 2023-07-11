using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;
using TaskManagement.Models.Entities;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Utility;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJWTAuthenticator _jwtAuthenticator;

        public AuthService(IServiceFactory serviceFactory)
        {

            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _userManager = _serviceFactory.GetService<UserManager<ApplicationUser>>();
            _roleManager = _serviceFactory.GetService<RoleManager<ApplicationRole>>();
            _jwtAuthenticator = _serviceFactory.GetService<IJWTAuthenticator>();
        }



        public async Task<AccountResponse> CreateUser(UserRegistrationRequest request)
        {

            ApplicationUser existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with Email {request.Email}");

            existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with username {request.UserName}");

            var roleName = DefaultRoleName.RoleName;
            var userRole = await _roleManager.FindByNameAsync(roleName);

            ApplicationUser user = new()
            {

                Email = request.Email.ToLower(),
                UserName = request.UserName.Trim().ToLower(),
                FirstName = request.Firstname.Trim(),
                LastName = request.LastName.Trim(),
                PhoneNumber = request.MobileNumber,
                Active = true,
                UserTypeId = userRole.Type
            };


            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var message = $"Failed to create user: {(result.Errors.FirstOrDefault())?.Description}";
                throw new InvalidOperationException(message);

            }

            string? role = user.UserTypeId.GetStringValue();
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (roleExist)
                await _userManager.AddToRoleAsync(user, role);
            else
                await _roleManager.CreateAsync(new ApplicationRole(role));


            return new AccountResponse
            {
                UserId = user.Id,
                UserName = user.UserName,
                Success = true,
                Message = "your account has been created"

            };

        }



        public async Task<AuthenticationResponse> UserLogin(LoginRequest request)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(request.UserName.ToLower().Trim());

            if (user == null)
                throw new InvalidOperationException("Invalid username or password");

            if (!user.Active)
                throw new InvalidOperationException("Account is not active");

            bool result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
                throw new InvalidOperationException("Invalid username or password");


            JwtToken userToken = await _jwtAuthenticator.GenerateJwtToken(user);


            string? userType = user.UserTypeId.GetStringValue();

            string fullName = string.IsNullOrWhiteSpace(user.MiddleName)
                ? $"{user.LastName} {user.FirstName}"
                : $"{user.LastName} {user.FirstName} {user.MiddleName}";


            return new AuthenticationResponse
            {
                JwtToken = userToken,
                UserType = userType,
                FullName = fullName,
                TwoFactor = false,
                UserId = user.Id
            };

        }

    }

}