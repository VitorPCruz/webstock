using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStock.Data.Migrations
{
    public partial class RecriandoSupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Addresses_AddressId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Suppliers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Suppliers",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Suppliers",
                type: "TEXT",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "Suppliers",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Suppliers",
                type: "TEXT",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Suppliers",
                type: "TEXT",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Suppliers",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Suppliers",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Suppliers");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Suppliers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Complement = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Neighborhood = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Addresses_AddressId",
                table: "Suppliers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
