using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class addNewColumnToPO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchID",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_BranchID",
                table: "PurchaseOrders",
                column: "BranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchID",
                table: "PurchaseOrders",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Branches_BranchID",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_BranchID",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "PurchaseOrders");
        }
    }
}
