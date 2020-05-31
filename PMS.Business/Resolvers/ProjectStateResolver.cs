using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Business.Resolvers
{
    public class ProjectStateResolver : IProjectStateResolver
    {
        public Enums.ProjectState GetProjectState(Domain.Project project)
        {
            var tasks = new List<Domain.Task>();
            GetChild(tasks, new List<Domain.Project> { project });

            if (tasks.All(t => t.StateId == (int)Enums.TaskState.Completed))
                return Enums.ProjectState.Completed;

            if (tasks.Any(t => t.StateId == (int)Enums.TaskState.inProgress))
                return Enums.ProjectState.inProgress;

            return Enums.ProjectState.Planned;
        }

        private void GetChild(List<Domain.Task> allTasks, List<Domain.Project> list)
        {
            foreach (var project in list)
            {
                if (project?.Children?.Any() ?? false)
                {
                    GetChild(allTasks, project.Children);
                }
                allTasks.AddRange(project.Tasks);
            }
        }
    }
}
