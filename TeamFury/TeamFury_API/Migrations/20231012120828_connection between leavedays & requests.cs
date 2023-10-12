using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class connectionbetweenleavedaysrequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveDays_AspNetUsers_UserId",
                table: "LeaveDays");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveDays_RequestTypes_RequestTypeID",
                table: "LeaveDays");

            migrationBuilder.DropColumn(
                name: "EmplyeeID",
                table: "LeaveDays");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LeaveDays",
                newName: "IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "RequestTypeID",
                table: "LeaveDays",
                newName: "RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveDays_UserId",
                table: "LeaveDays",
                newName: "IX_LeaveDays_IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveDays_RequestTypeID",
                table: "LeaveDays",
                newName: "IX_LeaveDays_RequestID");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "8f5096bb-633b-4151-9abd-704ed0fa5120");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a661800f-cf01-4e7a-b7ed-c7f36b93c18e", "4761d146-7fbe-451d-b6e7-d4a4d8e01200" });

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveDays_AspNetUsers_IdentityUserId",
                table: "LeaveDays",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveDays_Requests_RequestID",
                table: "LeaveDays",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveDays_AspNetUsers_IdentityUserId",
                table: "LeaveDays");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveDays_Requests_RequestID",
                table: "LeaveDays");

            migrationBuilder.RenameColumn(
                name: "RequestID",
                table: "LeaveDays",
                newName: "RequestTypeID");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "LeaveDays",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveDays_RequestID",
                table: "LeaveDays",
                newName: "IX_LeaveDays_RequestTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveDays_IdentityUserId",
                table: "LeaveDays",
                newName: "IX_LeaveDays_UserId");

            migrationBuilder.AddColumn<int>(
                name: "EmplyeeID",
                table: "LeaveDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "cf19d873-4243-4351-9fa1-9597afb6eaf2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "835aa163-2737-442a-8c01-d69f440f584e", "14296dfc-f7f1-43ab-8f30-be08c07caf25" });

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveDays_AspNetUsers_UserId",
                table: "LeaveDays",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveDays_RequestTypes_RequestTypeID",
                table: "LeaveDays",
                column: "RequestTypeID",
                principalTable: "RequestTypes",
                principalColumn: "RequestTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
