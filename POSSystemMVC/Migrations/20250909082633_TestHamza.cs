using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class TestHamza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderReceipts_PurchaseOrders_PurchaseOrderID",
                table: "PurchaseOrderReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchID",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorID",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Branches_BranchID",
                table: "Warehouses");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseID",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_WarehouseID",
                table: "InvoiceDetails",
                column: "WarehouseID");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Warehouses_WarehouseID",
                table: "InvoiceDetails",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderReceipts_PurchaseOrders_PurchaseOrderID",
                table: "PurchaseOrderReceipts",
                column: "PurchaseOrderID",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchID",
                table: "PurchaseOrders",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorID",
                table: "PurchaseOrders",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Branches_BranchID",
                table: "Warehouses",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Warehouses_WarehouseID",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderReceipts_PurchaseOrders_PurchaseOrderID",
                table: "PurchaseOrderReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchID",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorID",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Branches_BranchID",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_WarehouseID",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "WarehouseID",
                table: "InvoiceDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderReceipts_PurchaseOrders_PurchaseOrderID",
                table: "PurchaseOrderReceipts",
                column: "PurchaseOrderID",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchID",
                table: "PurchaseOrders",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorID",
                table: "PurchaseOrders",
                column: "VendorID",
                principalTable: "Vendors",
                principalColumn: "VendorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Branches_BranchID",
                table: "Warehouses",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
