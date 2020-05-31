using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain
{
    public class Task
    {
        public int Id { get; set; }
        //[ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        //[ForeignKey("ParentId")]
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        //[ForeignKey("StateId")]
        public int StateId { get; set; }
        public TaskState State { get; set; }
        public Project Project { get; set; }
        public Task Parent { get; set; }
        public List<Task> Children { get; set; }
    }
}
