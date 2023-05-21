using Demo.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Seeding
{
    public static class UsersSeeding
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            var admin = new ApplicationUser { Id = Guid.NewGuid() , UserName = "adminUser"  , Email = "Admin@gmail.com" };
            
            IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            admin.PasswordHash = passwordHasher.HashPassword(admin, "adminPassword*123");
            
            //adding admin User
            modelBuilder.Entity<ApplicationUser>()
                        .HasData(admin);

            //assigning the admin role to the admin
            modelBuilder.Entity<ApplicationUserRole>()
                        .HasData(new ApplicationUserRole { UserId = admin.Id, RoleId = new Guid("40de6547-e404-4c30-944f-85c15d27a204") });

        }
    }
}
