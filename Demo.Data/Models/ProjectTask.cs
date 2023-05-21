using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    public class ProjectTask
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; } = default!;

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = default!;

    }
}
