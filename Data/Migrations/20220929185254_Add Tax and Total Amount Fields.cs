using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoicingApp.Data.Migrations
{
    public partial class AddTaxandTotalAmountFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Tax",
                table: "InvoiceItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "InvoiceItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tax",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "InvoiceItems");
        }
    }
}
