using MediatR;
using PMS.Business.Queries;
using PMS.Business.Responses;
using PMS.DataAccess.Db;
using PMS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PMS.Business.Handlers
{
    public class GetProjectHandler : BaseHandler, IRequestHandler<GetProjectByIdQuery, ProjectResponse>
    {
        public GetProjectHandler(PmsContext dbContext) : base(dbContext)
        {

        }

        public async Task<ProjectResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await DbContext.Set<Project>().FindAsync(request.Id);
            return new ProjectResponse {
                Code = project.Code
            };
        }
    }
}
