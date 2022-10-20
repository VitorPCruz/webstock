using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStock.Data.Migrations
{
    public partial class AdicionandoProductAoStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Products_ProductId",
                table: "Storage");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Storage",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Products_ProductId",
                table: "Storage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Products_ProductId",
                table: "Storage");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Storage",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Products_ProductId",
                table: "Storage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
