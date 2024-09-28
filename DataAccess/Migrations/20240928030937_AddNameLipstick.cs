using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddNameLipstick : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShadeName",
                table: "Lipsticks",
                newName: "Usage");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Lipsticks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Lipsticks");

            migrationBuilder.RenameColumn(
                name: "Usage",
                table: "Lipsticks",
                newName: "ShadeName");
        }
    }
}
