using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Business.Responses
{
    public class ReportResponse
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public TaskResponse[] Tasks { get; set; }
    }

    public class TaskResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int ProjectId { get; set; }
    }
}
