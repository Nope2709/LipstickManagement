using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Accounts_AccountId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Addresses_AddressId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Payments_PaymentId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_AccountId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_AddressId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ShippedAddress",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ShippedDate",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ShippedFee",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OrderDetails",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "OrderDetails",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_PaymentId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: true),
                    AddressId = table.Column<int>(type: "integer", nullable: true),
                    PaymentId = table.Column<int>(type: "integer", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    ShippedFee = table.Column<double>(type: "double precision", nullable: true),
                    ShippedAddress = table.Column<string>(type: "text", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountId",
                table: "Order",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AddressId",
                table: "Order",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentId",
                table: "Order",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderDetails",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderDetails",
                newName: "PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_PaymentId");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "OrderDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "OrderDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "OrderDetails",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippedAddress",
                table: "OrderDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippedDate",
                table: "OrderDetails",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ShippedFee",
                table: "OrderDetails",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "OrderDetails",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AccountId",
                table: "OrderDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AddressId",
                table: "OrderDetails",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Accounts_AccountId",
                table: "OrderDetails",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Addresses_AddressId",
                table: "OrderDetails",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Payments_PaymentId",
                table: "OrderDetails",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
