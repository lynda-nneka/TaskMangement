using System;
namespace TaskManagement.Models.Dtos.Responses
{
    public class AccountResponse
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

