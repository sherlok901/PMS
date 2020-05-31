using MediatR;
using PMS.Business.Responses;
using System;
using System.Collections.Generic;

namespace PMS.Business.Queries
{
    public class GetProjectsInProgress : IRequest<ReportResponse>
    {
        public DateTime Date { get; set; }
    }
}
