using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int StateId { get; set; }
    }
}
