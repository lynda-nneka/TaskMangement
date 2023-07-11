using System;
namespace TaskManagement.Models.Dtos.Responses
{
    public class RoleResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ClaimCount { get; set; }
        public bool Active { get; set; }

    }
}

