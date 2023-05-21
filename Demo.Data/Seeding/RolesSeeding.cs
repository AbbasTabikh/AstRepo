using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Seeding
{
    public static class RolesSeeding
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {

            Guid adminRoleId = new Guid("40de6547-e404-4c30-944f-85c15d27a204");
            Guid userRoleId = new Guid("adb349e9-e268-487a-8ea9-e05a12a894c9");


            //adding the Application Roles
            modelBuilder.Entity<ApplicationRole>()
                    .HasData(
                        new ApplicationRole
                        {
                            Id = adminRoleId,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },

                        new ApplicationRole
                        {
                            Id = userRoleId,
                            Name = "User",
                            NormalizedName = "USER"
                        });
        }
    }
}
