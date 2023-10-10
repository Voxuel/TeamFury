using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class update_fail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "231764e1-717f-4977-a7a6-7abed5472538");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "32962815-8df1-48b1-bccb-392c33c72ec9", "AQAAAAEAACcQAAAAEHgG5npaEHV4sYl5/EXJOQGS2EQ/t5iu/obiES7PYzYArhm3y/Kv06FW7XGyxHHQOA==", "92cb17f5-b9e8-4f4b-a393-8d0440d73a2f", "Admin1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "59727953-c0f0-4c81-b9f7-16b09803937e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f14aa60b-9ca3-43bb-92e7-1637f8b486a6", "AQAAAAEAACcQAAAAEAA9ZBwzsYLm+UmlfNU1ie7B0dNcjK8XnjAHpUv2s6RashWGkKuChvFbxfAk7LHPKA==", "a697d668-3dfa-4c7f-b5db-cf9438a2ecb8", null });
        }
    }
}
