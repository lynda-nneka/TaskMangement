using System;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Dtos.Requests
{
    public class RoleRequest
    {
        [Required(ErrorMessage = "Role Name cannot be empty"), MinLength(2), MaxLength(30)]
        public string Name { get; set; }
        public UserType UserType { get; set; }
    }
}

