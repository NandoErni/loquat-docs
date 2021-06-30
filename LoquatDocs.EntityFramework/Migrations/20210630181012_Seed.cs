using Microsoft.EntityFrameworkCore.Migrations;

namespace LoquatDocs.EntityFramework.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                column: "Groupname",
                value: "Tax");

            migrationBuilder.InsertData(
                table: "Groups",
                column: "Groupname",
                value: "Banking");

            migrationBuilder.InsertData(
                table: "Groups",
                column: "Groupname",
                value: "Contracts");

            migrationBuilder.InsertData(
                table: "Groups",
                column: "Groupname",
                value: "Career");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Groupname",
                keyValue: "Banking");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Groupname",
                keyValue: "Career");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Groupname",
                keyValue: "Contracts");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Groupname",
                keyValue: "Tax");
        }
    }
}
