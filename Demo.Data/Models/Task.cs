using Demo.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Waiting;

        public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }
}
