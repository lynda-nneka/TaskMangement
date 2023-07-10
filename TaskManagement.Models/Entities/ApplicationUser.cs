using System;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Entities
{
    public class ApplicationUser :IdentityUser
    {
       
        public UserType UserTypeId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<TaskItem> AssignedTasks { get; set; }

        public  virtual ICollection<TaskItem> ReportedTasks { get; set; }

    }
}

