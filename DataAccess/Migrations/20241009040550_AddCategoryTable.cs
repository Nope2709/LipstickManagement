using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Payments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Payments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "OrderDetails",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "OrderDetails",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Lipsticks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Lipsticks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Lipsticks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Lipsticks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Lipsticks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LipstickIngredients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "LipstickIngredients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Ingredients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Ingredients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ImageURLs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ImageURLs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Feedbacks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Feedbacks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Customizations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Customizations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LipstickId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Lipsticks_LipstickId",
                        column: x => x.LipstickId,
                        principalTable: "Lipsticks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_LipstickId",
                table: "Category",
                column: "LipstickId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Lipsticks");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Lipsticks");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Lipsticks");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Lipsticks");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Lipsticks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LipstickIngredients");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "LipstickIngredients");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ImageURLs");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ImageURLs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Customizations");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Customizations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Accounts");
        }
    }
}
