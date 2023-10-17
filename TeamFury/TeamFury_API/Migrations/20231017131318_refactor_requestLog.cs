using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class refactor_requestLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestLogs_RequestLogID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestLogID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestLogID",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "MessageForDecline",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminName",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestID",
                table: "RequestLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "d30393e1-079b-443e-8f15-c0d1b2a4a7b4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b2b84999-c2cb-4aab-99db-6cb20d504021", "e6dcc96f-5b60-4201-b3b3-3db4e15f1516" });

            migrationBuilder.CreateIndex(
                name: "IX_RequestLogs_RequestID",
                table: "RequestLogs",
                column: "RequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLogs_Requests_RequestID",
                table: "RequestLogs",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLogs_Requests_RequestID",
                table: "RequestLogs");

            migrationBuilder.DropIndex(
                name: "IX_RequestLogs_RequestID",
                table: "RequestLogs");

            migrationBuilder.DropColumn(
                name: "RequestID",
                table: "RequestLogs");

            migrationBuilder.AlterColumn<string>(
                name: "MessageForDecline",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AdminName",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RequestLogID",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "7fcf1de8-1095-4a77-b55a-09002dea2484");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ab3c06f0-f542-49a8-8ea1-394953d572b9", "64fe5c96-38ef-48d8-9ad7-16f656810ec7" });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestLogID",
                table: "Requests",
                column: "RequestLogID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestLogs_RequestLogID",
                table: "Requests",
                column: "RequestLogID",
                principalTable: "RequestLogs",
                principalColumn: "RequestLogID");
        }
    }
}
