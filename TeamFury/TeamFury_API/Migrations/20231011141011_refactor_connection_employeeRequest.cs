using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class refactor_connection_employeeRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "EmployeesRequest",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "FK_EmployeesRequest_AspNetUsers_IdentityUserId",
                table: "EmployeesRequest",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesRequest_Requests_RequestId",
                table: "EmployeesRequest",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesRequest_AspNetUsers_IdentityUserId",
                table: "EmployeesRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesRequest_Requests_RequestId",
                table: "EmployeesRequest");

            migrationBuilder.DropIndex(
                name: "IX_EmployeesRequest_IdentityUserId",
                table: "EmployeesRequest");

            migrationBuilder.DropIndex(
                name: "IX_EmployeesRequest_RequestId",
                table: "EmployeesRequest");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "EmployeesRequest");

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
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b4597da1-b9b1-4a83-91f7-1cc09eb253a0", "b21ac45b-86a6-44a5-9843-b9c09b9947e0" });
        }
    }
}
