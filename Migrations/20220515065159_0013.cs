using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpentryShop.Migrations
{
    public partial class _0013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Boxes_BoxId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BoxId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Boxes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_ProductId",
                table: "Boxes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Products_ProductId",
                table: "Boxes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Products_ProductId",
                table: "Boxes");

            migrationBuilder.DropIndex(
                name: "IX_Boxes_ProductId",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Boxes");

            migrationBuilder.AddColumn<Guid>(
                name: "BoxId",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BoxId",
                table: "Products",
                column: "BoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Boxes_BoxId",
                table: "Products",
                column: "BoxId",
                principalTable: "Boxes",
                principalColumn: "Id");
        }
    }
}
