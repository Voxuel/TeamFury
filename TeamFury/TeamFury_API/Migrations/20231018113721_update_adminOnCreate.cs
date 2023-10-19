using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class update_adminOnCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c9cfbde-730a-4217-93ea-6d8fba1ee541", "4cf4c471-b32e-4848-a820-2bf9e326d052", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541");
        }
    }
}
