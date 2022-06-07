using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.DataBase.Context.Migrations
{
    public partial class ApplicationChildern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegeratedApplication");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "Application",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_ApplicationId",
                table: "Application",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Application_ApplicationId",
                table: "Application",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Application_ApplicationId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_ApplicationId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Application");

            migrationBuilder.CreateTable(
                name: "IntegeratedApplication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    BaseApplicationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegeratedApplication", x => x.Id);
                });
        }
    }
}
