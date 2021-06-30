using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoquatDocs.EntityFramework.Migrations
{
    public partial class All_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Documents",
                newName: "DocumentDate");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Documents",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "Documents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentAdded",
                table: "Documents",
                type: "TEXT",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<string>(
                name: "Groupname",
                table: "Documents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "DocumentPath");

            migrationBuilder.CreateTable(
                name: "AliasGroups",
                columns: table => new
                {
                    AliasGroupName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AliasGroups", x => x.AliasGroupName);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Groupname = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Groupname);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    DocumentPath = table.Column<string>(type: "TEXT", nullable: false),
                    IsPayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.DocumentPath);
                    table.ForeignKey(
                        name: "FK_Invoices_Documents_DocumentPath",
                        column: x => x.DocumentPath,
                        principalTable: "Documents",
                        principalColumn: "DocumentPath",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<string>(type: "TEXT", nullable: false),
                    DocumentPath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tags_Documents_DocumentPath",
                        column: x => x.DocumentPath,
                        principalTable: "Documents",
                        principalColumn: "DocumentPath",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aliases",
                columns: table => new
                {
                    AliasName = table.Column<string>(type: "TEXT", nullable: false),
                    AliasGroupName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aliases", x => new { x.AliasName, x.AliasGroupName });
                    table.ForeignKey(
                        name: "FK_Aliases_AliasGroups_AliasGroupName",
                        column: x => x.AliasGroupName,
                        principalTable: "AliasGroups",
                        principalColumn: "AliasGroupName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Groupname",
                table: "Documents",
                column: "Groupname");

            migrationBuilder.CreateIndex(
                name: "IX_Aliases_AliasGroupName",
                table: "Aliases",
                column: "AliasGroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DocumentPath",
                table: "Tags",
                column: "DocumentPath");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Groups_Groupname",
                table: "Documents",
                column: "Groupname",
                principalTable: "Groups",
                principalColumn: "Groupname",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Groups_Groupname",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "Aliases");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "AliasGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_Groupname",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentAdded",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Groupname",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentDate",
                table: "Documents",
                newName: "Path");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Documents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Path");
        }
    }
}
