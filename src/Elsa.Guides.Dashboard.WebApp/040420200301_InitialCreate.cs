using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elsa.Guides.Dashboard.WebApp
{
    partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "admin");
            migrationBuilder.CreateTable(
                name: "__EFMigrationsHistory",
                schema: "admin",
                columns: table => new
                {
                    MigrationId = table.Column<string>(nullable: false),
                    ProductVersion = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___EFMigrationsHistory", x => x.MigrationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__EFMigrationsHistory",
                schema: "admin");
        }
    }
}