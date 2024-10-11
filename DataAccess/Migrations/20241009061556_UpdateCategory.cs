using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Lipsticks_LipstickId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LipstickId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LipstickId",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Lipsticks_CategoryId",
                table: "Lipsticks",
                column: "CategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lipsticks_Categories_CategoryId",
                table: "Lipsticks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lipsticks_Categories_CategoryId",
                table: "Lipsticks");

            migrationBuilder.DropIndex(
                name: "IX_Lipsticks_CategoryId",
                table: "Lipsticks");

            migrationBuilder.AddColumn<int>(
                name: "LipstickId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LipstickId",
                table: "Categories",
                column: "LipstickId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Lipsticks_LipstickId",
                table: "Categories",
                column: "LipstickId",
                principalTable: "Lipsticks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
