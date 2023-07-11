using System;
namespace TaskManagement.Models.Dtos.Responses
{
    public class AuthResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string error { get; set; }
    }
}

