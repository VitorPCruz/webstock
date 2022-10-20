using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStock.Data.Migrations
{
    public partial class MudandoStorageParaReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Storage",
                newName: "ProductQuantity");

            migrationBuilder.AddColumn<int>(
                name: "Operation",
                table: "Storage",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operation",
                table: "Storage");

            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "Storage",
                newName: "Quantity");
        }
    }
}
