using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();

    }
}
