using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStock.Data.Migrations
{
    public partial class RemodelandoReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "Reports",
                newName: "OperationQuantity");

            migrationBuilder.AddColumn<int>(
                name: "AfterOperation",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BeforeOperation",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterOperation",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "BeforeOperation",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "OperationQuantity",
                table: "Reports",
                newName: "ProductQuantity");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Reports",
                type: "TEXT",
                nullable: true);
        }
    }
}
