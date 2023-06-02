using Demo.Data.Configurations;
using Demo.Data.Models;
using Demo.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {

        public DbSet<Models.Task> Tasks  { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }


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

            // ContactRequest
            builder.Entity<ContactRequest>().HasKey(x => x.Id);
            builder.Entity<ContactRequest>().Property(x => x.UserFullName).IsRequired().HasMaxLength(56);
            builder.Entity<ContactRequest>().Property(x => x.Content).IsRequired();
            builder.Entity<ContactRequest>().Property(x => x.Email).IsRequired(false);
            builder.Entity<ContactRequest>().Property(x => x.IPAddress).IsRequired(false);
            builder.Entity<ContactRequest>().Property(x => x.PhoneNumber).IsRequired(false);




            // Roles must be seeded  before users (due to UserRoles Relationship)
            builder.SeedRoles();
            builder.SeedUsers();
            builder.SeedMunicipalities();


        }
    }
}
