using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class fixSODetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SODetailID",
                table: "SalesOrderDetails",
                newName: "SalesOrderDetailsID");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SalesOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SalesOrderDetails");

            migrationBuilder.RenameColumn(
                name: "SalesOrderDetailsID",
                table: "SalesOrderDetails",
                newName: "SODetailID");
        }
    }
}
