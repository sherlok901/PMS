using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Business.Queries;
using PMS.Business.Resolvers;
using PMS.Business.Responses;
using PMS.DataAccess.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace PMS.Business.Handlers
{
    public class GetReportHandler : BaseHandler, IRequestHandler<GetProjectsInProgress, List<ReportResponse>>
    {
        private readonly IProjectStateResolver _projectStateResolver;
        public GetReportHandler(PmsContext dbContext, IProjectStateResolver projectStateResolver) : base(dbContext)
        {
            _projectStateResolver = projectStateResolver;
        }

        public async Task<List<ReportResponse>> Handle(GetProjectsInProgress request, CancellationToken cancellationToken)
        {
            var result = new List<ReportResponse>();
            var projects = await DbContext.Project.AsNoTracking()
                .Include(i => i.Tasks)
                .Include(i => i.Children)
                    .ThenInclude(it => it.Tasks)
                .ToListAsync();

            foreach (var project in projects)
            {
                var tasks = _projectStateResolver.GetProjectTasks(project);
                var state = _projectStateResolver.GetProjectState(tasks);
                if(state == Enums.ProjectState.inProgress)
                {
                    var tasksResponse = new List<TaskResponse>();

                    tasksResponse.AddRange(
                        tasks.Where(i => i.StateId == (int)Enums.TaskState.inProgress)
                        .Select(n => new TaskResponse {
                            Id = n.Id,
                            FinishDate = n.FinishDate,
                            Name = n.Name,
                            StartDate = n.StartDate,
                            ProjectId = n.ProjectId
                        })
                    );

                    result.Add(new ReportResponse
                    {
                        FinishDate = project.FinishDate,
                        Name = project.Name,
                        ProjectId = project.Id,
                        StartDate = project.StartDate,
                        Tasks = tasksResponse.ToArray()
                    });
                }
            }

            return await Task.FromResult(result);


            //var wb = new XLWorkbook();
            ////DataTable dt = GetDataTableOrWhatever();
            //wb.Worksheets.Add(dt, "WorksheetName");
            //wb.Worksheets.Add()
        }
    }
}
