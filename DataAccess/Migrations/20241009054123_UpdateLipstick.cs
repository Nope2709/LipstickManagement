using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdateLipstick : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Lipsticks_LipstickId",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "LipstickId",
                table: "Lipsticks");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Lipsticks");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Category_LipstickId",
                table: "Categories",
                newName: "IX_Categories_LipstickId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Lipsticks",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LipstickId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Lipsticks_LipstickId",
                table: "Categories",
                column: "LipstickId",
                principalTable: "Lipsticks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Lipsticks_LipstickId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Lipsticks");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_LipstickId",
                table: "Category",
                newName: "IX_Category_LipstickId");

            migrationBuilder.AddColumn<int>(
                name: "LipstickId",
                table: "Lipsticks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Lipsticks",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LipstickId",
                table: "Category",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Lipsticks_LipstickId",
                table: "Category",
                column: "LipstickId",
                principalTable: "Lipsticks",
                principalColumn: "Id");
        }
    }
}
