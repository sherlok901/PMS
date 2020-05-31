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
using System.Data;
using System.IO;

namespace PMS.Business.Handlers
{
    public class GetReportHandler : BaseHandler, IRequestHandler<GetProjectsInProgress, ReportResponse>
    {
        private readonly IProjectStateResolver _projectStateResolver;
        public GetReportHandler(PmsContext dbContext, IProjectStateResolver projectStateResolver) : base(dbContext)
        {
            _projectStateResolver = projectStateResolver;
        }

        public async Task<ReportResponse> Handle(GetProjectsInProgress request, CancellationToken cancellationToken)
        {
            var dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Project");
            dt.Columns.Add("Task");

            var result = new List<ReportResponse>();
            var projects = await DbContext.Project.AsNoTracking()
                .Include(i => i.Tasks)
                .Include(i => i.Children)
                    .ThenInclude(it => it.Tasks)
                .ToListAsync();

            foreach (var project in projects)
            {
                var tasks = _projectStateResolver.GetProjectTasks(project);
                var projectState = _projectStateResolver.GetProjectState(tasks);
                if(projectState == Enums.ProjectState.inProgress)
                {
                    var filteredTasks = tasks.Where(i => i.StateId == (int)Enums.TaskState.inProgress
                        && i.StartDate <= request.Date && i.FinishDate >= request.Date).ToList();

                    filteredTasks.ForEach(t =>
                    {
                        dt.Rows.Add(new object[] { project.Name, t.Name });
                    });

                    dt.AcceptChanges();
                }
            }


            var wb = new XLWorkbook();
            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Style.Font.Bold = true;

            var wsh = wb.Worksheets.Add(dt, "Report");
            //wsh.Cell("A1").Value = "Hello World!";
            //wsh.Cell("A2").FormulaA1 = "=MID(A1, 7, 5)";

            var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Flush();


            return await Task.FromResult(new ReportResponse { FileData = stream});
        }
    }
}
