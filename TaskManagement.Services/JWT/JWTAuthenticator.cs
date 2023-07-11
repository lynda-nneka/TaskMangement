﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Models.Entities;
using TaskManagement.Services.Interfaces;
using TaskManagement.Models.Dtos.Responses;
using TaskManagement.Models.Enums;

namespace TaskManagement.Services.JWT
{
    public class JwtAuthenticator : IJWTAuthenticator
    {
        private readonly IConfiguration _configuration;

        public JwtAuthenticator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<JwtToken> GenerateJwtToken(ApplicationUser user, string expires = null,
            List<Claim> additionalClaims = null)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();
            string jwtConfig = _configuration.GetSection("JwtConfig:Key").Value;

            var key = Encoding.ASCII.GetBytes(jwtConfig);
            string userRole = user.UserTypeId.GetStringValue();

            IdentityOptions _options = new();

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName),
                new Claim(ClaimTypes.Role, userRole)
            };

            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }

            string issuer = _configuration.GetSection("JwtConfig:Issuer").Value;
            string audience = _configuration.GetSection("JwtConfig:Audience").Value;
            string expire = _configuration.GetSection("JwtConfig:Expires").Value;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = string.IsNullOrWhiteSpace(expires)
                    ? DateTime.Now.AddHours(double.Parse(expire))
                    : DateTime.Now.AddMinutes(double.Parse(expires)),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return new JwtToken
            {
                Token = jwtToken,
                Issued = DateTime.Now,
                Expires = tokenDescriptor.Expires
            };

        }
    }
}
