using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebClothes.Migrations
{
    /// <inheritdoc />
    public partial class updatedborder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "SaleOrderLines",
                newName: "SaleOrderId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "SaleOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderLines_ProductId",
                table: "SaleOrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrderLines_SaleOrderId",
                table: "SaleOrderLines",
                column: "SaleOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderLines_Products_ProductId",
                table: "SaleOrderLines",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderLines_SaleOrders_SaleOrderId",
                table: "SaleOrderLines",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderLines_Products_ProductId",
                table: "SaleOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderLines_SaleOrders_SaleOrderId",
                table: "SaleOrderLines");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrderLines_ProductId",
                table: "SaleOrderLines");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrderLines_SaleOrderId",
                table: "SaleOrderLines");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "SaleOrders");

            migrationBuilder.RenameColumn(
                name: "SaleOrderId",
                table: "SaleOrderLines",
                newName: "OrderId");
        }
    }
}
