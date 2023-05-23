using Demo.Data.Configurations;
using Demo.Data.Models;
using Demo.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {

        public DbSet<Models.Task> Tasks  { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }

        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ConfigureMunicipality();
            builder.ConfigureProject();
            builder.ConfigureTask();
            builder.ConfigureUserRole();
            builder.ConfigureProjectUser();
            builder.ConfigureProjectTask();

            // Roles must be seeded  before users (due to UserRoles Relationship)
            builder.SeedRoles();
            builder.SeedUsers();
            builder.SeedMunicipalities();


        }
    }
}
