using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class FixAllRelationss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderReceipts_Warehouses_WarehouseID",
                table: "PurchaseOrderReceipts");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderReceipts_Warehouses_WarehouseID",
                table: "PurchaseOrderReceipts",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderReceipts_Warehouses_WarehouseID",
                table: "PurchaseOrderReceipts");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderReceipts_Warehouses_WarehouseID",
                table: "PurchaseOrderReceipts",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
