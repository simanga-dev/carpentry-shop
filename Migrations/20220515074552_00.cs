using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpentryShop.Migrations
{
    public partial class _00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Products_ProductId",
                table: "Boxes");

            migrationBuilder.DropIndex(
                name: "IX_Boxes_ProductId",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Boxes",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Boxes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Boxes");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Boxes",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
    }
}
