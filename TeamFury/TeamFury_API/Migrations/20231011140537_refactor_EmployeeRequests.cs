using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class refactor_EmployeeRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "EmployeesRequest",
                newName: "RequestId");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "EmployeesRequest",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "EmployeesRequest",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "986a9047-92be-4dee-aaae-fa34b038ad39");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4597da1-b9b1-4a83-91f7-1cc09eb253a0", null, "b21ac45b-86a6-44a5-9843-b9c09b9947e0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "EmployeesRequest",
                newName: "RequestID");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeesRequest",
                newName: "EmployeeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmployeesRequest",
                newName: "ID");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32962815-8df1-48b1-bccb-392c33c72ec9", "AQAAAAEAACcQAAAAEHgG5npaEHV4sYl5/EXJOQGS2EQ/t5iu/obiES7PYzYArhm3y/Kv06FW7XGyxHHQOA==", "92cb17f5-b9e8-4f4b-a393-8d0440d73a2f" });
        }
    }
}
