using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamFury_API.Migrations
{
    public partial class refactor_requestEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9cfbde-730a-4217-93ea-6d8fba1ee541",
                column: "ConcurrencyStamp",
                value: "2ec2cf8b-657e-4609-bf00-c1ea06521be0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cef773a-6124-4182-a8ad-3567cd037ea7",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "91bf57ad-cd68-4fb5-a690-05918d1e0552", "b875b79f-a17d-483a-9f70-1e8c1abd6873" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
