using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }
}
