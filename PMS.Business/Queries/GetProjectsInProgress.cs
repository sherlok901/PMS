using MediatR;
using PMS.Business.Responses;
using System.Collections.Generic;

namespace PMS.Business.Queries
{
    public class GetProjectsInProgress : IRequest<List<ReportResponse>>
    {
    }
}
