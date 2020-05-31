using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Business.Queries;
using PMS.Business.Responses;
using PMS.DataAccess.Db;
using PMS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using PMS.Business.Resolvers;

namespace PMS.Business.Handlers
{
    public class GetProjectHandler : BaseHandler, IRequestHandler<GetProjectByIdQuery, ProjectResponse>
    {
        private readonly IProjectStateResolver _projectStateResolver;
        public GetProjectHandler(PmsContext dbContext, IProjectStateResolver projectStateResolver) : base(dbContext)
        {
            _projectStateResolver = projectStateResolver;
        }

        public async Task<ProjectResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await DbContext.Project.AsNoTracking()
                .Include(i => i.Tasks)
                .Include(i => i.Children)
                    .ThenInclude(it => it.Tasks)
                .FirstOrDefaultAsync(i => i.Id == request.Id);

            var state = _projectStateResolver.GetProjectState(project);

            return new ProjectResponse {
                Code = project.Code,
                FinishDate = project.FinishDate,
                StartDate = project.StartDate,
                Id = project.Id,
                State = state.ToString(),
                Name = project.Name
            };
        }

        
    }
}
