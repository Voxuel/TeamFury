using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class add_userIdentity_and_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c9cfbde-730a-4217-93ea-6d8fba1ee541", "cb9e7b1a-fd75-41bd-b70b-d934607feed2", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6cef773a-6124-4182-a8ad-3567cd037ea7", 0, "f41237a1-e57f-49b3-ba34-1809548ea800", "trolllovecookies@gmail.com", false, false, null, null, null, "AQAAAAEAACcQAAAAEGyoFhVBQCo3hdkGtYe6JN/Z2NhU6ZM7hzVKYL2TtJFAt3N+KJ9ZVZ2xYk3BZ4AIcQ==", null, false, "d0d88ddd-f0f5-464f-ae06-9e63b42b505b", false, "Admin1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6c9cfbde-730a-4217-93ea-6d8fba1ee541", "6cef773a-6124-4182-a8ad-3567cd037ea7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6c9cfbde-730a-4217-93ea-6d8fba1ee541", "6cef773a-6124-4182-a8ad-3567cd037ea7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7");
        }
    }
}
