using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class new1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseID",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_WarehouseID",
                table: "Invoices",
                column: "WarehouseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Warehouses_WarehouseID",
                table: "Invoices",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Warehouses_WarehouseID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_WarehouseID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "WarehouseID",
                table: "Invoices");
        }
    }
}
