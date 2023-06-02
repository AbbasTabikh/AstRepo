using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Data.Migrations
{
    /// <inheritdoc />
    public partial class Recaptcha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("40de6547-e404-4c30-944f-85c15d27a204"), new Guid("a7814749-fabf-4686-94c0-f871650aa29b") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a7814749-fabf-4686-94c0-f871650aa29b"));

            migrationBuilder.CreateTable(
                name: "ContactRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFullName = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequests", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("8812476e-c5f3-4ace-a07d-280415a7636b"), 0, "55beb14a-b206-4836-a2b9-8ddb389acfb3", "Admin@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEBTkA2OqErntnxFjco1BDP3h/ygoCl2lbvOZeLFfHzBiwWnJdyh9xWMnnErdWCD+UA==", null, false, null, false, "adminUser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("40de6547-e404-4c30-944f-85c15d27a204"), new Guid("8812476e-c5f3-4ace-a07d-280415a7636b") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactRequests");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("40de6547-e404-4c30-944f-85c15d27a204"), new Guid("8812476e-c5f3-4ace-a07d-280415a7636b") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8812476e-c5f3-4ace-a07d-280415a7636b"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a7814749-fabf-4686-94c0-f871650aa29b"), 0, "453b3336-d73d-4204-839d-3f1b1b83d75b", "Admin@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEHgkEVcedklvEtg3FAZV88iW0gySEKjpsaa0xVVpGl4HTRDj9iL+8Ya4ULS0AuogdA==", null, false, null, false, "adminUser" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("40de6547-e404-4c30-944f-85c15d27a204"), new Guid("a7814749-fabf-4686-94c0-f871650aa29b") });
        }
    }
}
