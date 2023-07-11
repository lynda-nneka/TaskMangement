using System;
namespace TaskManagement.Models.Dtos.Responses
{
    public class AuthenticationResponse
    {
        public JwtToken JwtToken { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public bool TwoFactor { get; set; }
        public string UserId { get; set; }

    }
}

