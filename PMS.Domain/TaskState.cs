using System.Collections.Generic;

namespace PMS.Domain
{
    public class TaskState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Task> Task { get; set; }
    }
}
