using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.DataBase.Context.Migrations
{
    public partial class ChangeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionsRoles_ActionsRoles_ActionId",
                table: "ActionsRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionsRoles_PageAction_PageActionId",
                table: "ActionsRoles");

            migrationBuilder.DropIndex(
                name: "IX_ActionsRoles_PageActionId",
                table: "ActionsRoles");

            migrationBuilder.DropColumn(
                name: "PageActionId",
                table: "ActionsRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsRoles_PageAction_ActionId",
                table: "ActionsRoles",
                column: "ActionId",
                principalTable: "PageAction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionsRoles_PageAction_ActionId",
                table: "ActionsRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "PageActionId",
                table: "ActionsRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActionsRoles_PageActionId",
                table: "ActionsRoles",
                column: "PageActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsRoles_ActionsRoles_ActionId",
                table: "ActionsRoles",
                column: "ActionId",
                principalTable: "ActionsRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsRoles_PageAction_PageActionId",
                table: "ActionsRoles",
                column: "PageActionId",
                principalTable: "PageAction",
                principalColumn: "Id");
        }
    }
}
