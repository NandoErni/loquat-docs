using Microsoft.EntityFrameworkCore.Migrations;

namespace LoquatDocs.EntityFramework.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Documents_DocumentPath",
                table: "Tags");

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

            migrationBuilder.InsertData(
                table: "Groups",
                column: "Groupname",
                value: "Miscellaneous");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Documents_DocumentPath",
                table: "Tags",
                column: "DocumentPath",
                principalTable: "Documents",
                principalColumn: "DocumentPath",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Documents_DocumentPath",
                table: "Tags");

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Groupname",
                keyValue: "Miscellaneous");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Documents_DocumentPath",
                table: "Tags",
                column: "DocumentPath",
                principalTable: "Documents",
                principalColumn: "DocumentPath",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
