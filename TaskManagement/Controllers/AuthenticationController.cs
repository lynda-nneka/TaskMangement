using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Models.Dtos.Requests;
using TaskManagement.Models.Dtos.Responses;
using TaskManagement.Services.Infrastructure;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Authorization")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("create-an-account", Name = "Create-an-Acoount")]
        [SwaggerOperation(Summary = "Creates new user account")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user", Type = typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User with provided email already exists", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateAccount(UserRegistrationRequest request)
        {
            AccountResponse response = await _authService.CreateUser(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [SwaggerOperation(Summary = "Authenticates user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns user Id", Type = typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<AuthenticationResponse>> Login(LoginRequest request)
        {
            AuthenticationResponse response = await _authService.UserLogin(request);
            return Ok(response);
        }
    }
}
