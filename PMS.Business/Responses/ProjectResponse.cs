using System;

namespace PMS.Business.Responses
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
    }
}
