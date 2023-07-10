using System;
using TaskManagement.Models.Enums;
using static System.Collections.Specialized.BitVector32;

namespace TaskManagement.Models.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status TaskStatus { get; set; }
        public string AssigneeId { get; set; }
        public ApplicationUser Assignee { get; set; }
        public DateTime DueTime { get; set; }
        public string ReporterId { get; set; }
        public ApplicationUser Reporter { get; set; }
        public Priority Priority { get; set; }


    }
}

