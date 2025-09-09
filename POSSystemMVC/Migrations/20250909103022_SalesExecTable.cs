using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSSystemMVC.Migrations
{
    /// <inheritdoc />
    public partial class SalesExecTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesExecutiveID",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalesExecutives",
                columns: table => new
                {
                    SalesExecutiveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    InvoiceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesExecutives", x => x.SalesExecutiveID);
                    table.ForeignKey(
                        name: "FK_SalesExecutives_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesExecutives_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SalesExecutiveID",
                table: "Invoices",
                column: "SalesExecutiveID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesExecutives_BranchID",
                table: "SalesExecutives",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesExecutives_InvoiceID",
                table: "SalesExecutives",
                column: "InvoiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_SalesExecutives_SalesExecutiveID",
                table: "Invoices",
                column: "SalesExecutiveID",
                principalTable: "SalesExecutives",
                principalColumn: "SalesExecutiveID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_SalesExecutives_SalesExecutiveID",
                table: "Invoices");

            migrationBuilder.DropTable(
                name: "SalesExecutives");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SalesExecutiveID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SalesExecutiveID",
                table: "Invoices");
        }
    }
}
