using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Configurations
{
    public static class Configurations
    {
        public static void ConfigureProject(this ModelBuilder builder)
        {
            builder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.Description)
                      .HasMaxLength(100);
            });
        }
        public static void ConfigureTask(this ModelBuilder builder)
        {
            builder.Entity<Models.Task>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Description)
                      .HasMaxLength(100);

                entity.Property(p => p.Title)
                      .IsRequired()
                      .HasMaxLength(30);

                entity.Property(p => p.Status)
                      .HasConversion<byte>();
            });
        }


        //Many to Many
        public static void ConfigureUserRole(this ModelBuilder builder)
        {
            builder.Entity<ApplicationUserRole>()
                   .HasKey(userRole => new { userRole.UserId, userRole.RoleId });

            builder.Entity<ApplicationUserRole>()
                   .HasOne(userRole => userRole.User)
                   .WithMany(user => user.UserRoles)
                   .HasForeignKey(userRole => userRole.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUserRole>()
                   .HasOne(userRole => userRole.Role)
                   .WithMany(role => role.UserRoles)
                   .HasForeignKey(userRole => userRole.RoleId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
        public static void ConfigureProjectTask(this  ModelBuilder builder)
        {
            builder.Entity<ProjectTask>()
                   .HasKey(pt => new { pt.TaskId, pt.ProjectId });

            builder.Entity<ProjectTask>()
                    .HasOne(pt => pt.Task)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(pt => pt.TaskId);

            builder.Entity<ProjectTask>()
                   .HasOne(pt => pt.Project)
                   .WithMany(p => p.ProjectTasks)
                   .HasForeignKey(pt => pt.ProjectId);

        }
        public static void ConfigureProjectUser(this ModelBuilder builder)
        {
            builder.Entity<ProjectUser>()
                   .HasKey(pu => new { pu.ProjectId, pu.UserId });

            builder.Entity<ProjectUser>()
                   .HasOne(pu => pu.Project)
                   .WithMany(p => p.ProjectUsers)
                   .HasForeignKey(pu => pu.ProjectId);

            builder.Entity<ProjectUser>()
                   .HasOne(pu => pu.User)
                   .WithMany(u => u.ProjectUsers)
                   .HasForeignKey(pu => pu.UserId);
        }
    }
}
