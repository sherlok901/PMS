using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using PMS.Business.Responses;

namespace PMS.Business.Commands
{
    public class UpdateProjectCommand : IRequest<ProjectResponse>
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string State { get; set; }
    }
}
