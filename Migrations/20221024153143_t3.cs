using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStock.Migrations
{
    public partial class t3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                type: "varchar(100)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "Suppliers",
                type: "varchar(100)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Products",
                type: "varchar(100)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "varchar(100)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodeBar",
                table: "Products",
                type: "varchar(100)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldMaxLength: 35);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "Suppliers",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Products",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodeBar",
                table: "Products",
                type: "varchar(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 35);
        }
    }
}
