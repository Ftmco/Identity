using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.DataBase.Context.Migrations
{
    public partial class PageAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PageAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageAction_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionsRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PageActionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsRoles_ActionsRoles_ActionId",
                        column: x => x.ActionId,
                        principalTable: "ActionsRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionsRoles_PageAction_PageActionId",
                        column: x => x.PageActionId,
                        principalTable: "PageAction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActionsRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionsRoles_ActionId",
                table: "ActionsRoles",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionsRoles_PageActionId",
                table: "ActionsRoles",
                column: "PageActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionsRoles_RoleId",
                table: "ActionsRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PageAction_PageId",
                table: "PageAction",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionsRoles");

            migrationBuilder.DropTable(
                name: "PageAction");
        }
    }
}
