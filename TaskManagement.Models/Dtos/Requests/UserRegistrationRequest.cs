using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.Dtos.Requests
{
    public class UserRegistrationRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string MobileNumber { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string LastName { get; set; }

       
    }
}

