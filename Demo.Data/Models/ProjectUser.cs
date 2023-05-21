using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    public class ProjectUser
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = default!;

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = default!;
    }
}
