using System;
namespace TaskManagement.Models.Entities
{
    public class BaseEntity
    {
       public Guid Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}

