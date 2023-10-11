using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class refactor_ids_for_connections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesRequest_Requests_RequestId",
                table: "EmployeesRequest");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeesRequest");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "EmployeesRequest",
                newName: "RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeesRequest_RequestId",
                table: "EmployeesRequest",
                newName: "IX_EmployeesRequest_RequestID");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "c1f4d5a8-f4ca-4763-8beb-7b045c9ffb25");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "329706a2-3095-436e-b151-61de9aaed9ed", "3adab785-226b-409a-aaa8-bd5d3d485459" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesRequest_Requests_RequestID",
                table: "EmployeesRequest",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesRequest_Requests_RequestID",
                table: "EmployeesRequest");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "EmployeesRequest",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeesRequest_RequestID",
                table: "EmployeesRequest",
                newName: "IX_EmployeesRequest_RequestId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmployeesRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "1d447e1a-d890-447b-94fe-fdeb548b7843");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c6c810a6-72b3-48e6-ac4f-8432dfe3891c", "99005caa-7eac-4d89-9916-b1fd66ee077e" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesRequest_Requests_RequestId",
                table: "EmployeesRequest",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
