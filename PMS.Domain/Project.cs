using System;
using System.Collections.Generic;

namespace PMS.Domain
{
    public class Project
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public List<Task> Tasks { get; set; }
        public Project Parent { get; set; }
        public List<Project> Children { get; set; }
    }
}
