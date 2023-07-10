using System;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Dtos.Requests
{
    public class UpdatetaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status TaskStatus { get; set; }
        public string AssigneeId { get; set; }
        public DateTime DueTime { get; set; }
        public string ReporterId { get; set; }
        public Priority Priority { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

