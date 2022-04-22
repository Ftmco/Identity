using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.DataBase.Context.Migrations
{
    public partial class UpdateProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FistName",
                table: "Profile",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Profile",
                newName: "FistName");
        }
    }
}
