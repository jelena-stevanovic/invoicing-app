using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoicingApp.Data.Migrations
{
    public partial class AddOrdinalNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Index",
                table: "InvoiceItems",
                newName: "OrdinalNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrdinalNumber",
                table: "InvoiceItems",
                newName: "Index");
        }
    }
}
