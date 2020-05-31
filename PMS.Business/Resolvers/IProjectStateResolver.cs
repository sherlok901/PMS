using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Business.Resolvers
{
    public interface IProjectStateResolver
    {
        Enums.ProjectState GetProjectState(Domain.Project project);
        Enums.ProjectState GetProjectState(List<Domain.Task> tasks);

        List<Domain.Task> GetProjectTasks(Domain.Project project);
    }
}
