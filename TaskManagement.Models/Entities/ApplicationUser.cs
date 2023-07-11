using System;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Entities
{
    public class ApplicationUser :IdentityUser
    {

        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? RecoveryMail { get; set; }
        public bool Active { get; set; }
        public UserType UserTypeId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<TaskItem> AssignedTasks { get; set; }

        public  virtual ICollection<TaskItem> ReportedTasks { get; set; }

    }
}

