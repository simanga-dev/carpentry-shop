using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpentryShop.Migrations
{
    public partial class _033w : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxs_Boxes_BoxId",
                table: "OrderBoxs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxs_Orders_OrderId",
                table: "OrderBoxs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBoxs",
                table: "OrderBoxs");

            migrationBuilder.RenameTable(
                name: "OrderBoxs",
                newName: "OrderBoxes");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBoxs_OrderId",
                table: "OrderBoxes",
                newName: "IX_OrderBoxes_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBoxs_BoxId",
                table: "OrderBoxes",
                newName: "IX_OrderBoxes_BoxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBoxes",
                table: "OrderBoxes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxes_Boxes_BoxId",
                table: "OrderBoxes",
                column: "BoxId",
                principalTable: "Boxes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxes_Orders_OrderId",
                table: "OrderBoxes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxes_Boxes_BoxId",
                table: "OrderBoxes");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderBoxes_Orders_OrderId",
                table: "OrderBoxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBoxes",
                table: "OrderBoxes");

            migrationBuilder.RenameTable(
                name: "OrderBoxes",
                newName: "OrderBoxs");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBoxes_OrderId",
                table: "OrderBoxs",
                newName: "IX_OrderBoxs_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBoxes_BoxId",
                table: "OrderBoxs",
                newName: "IX_OrderBoxs_BoxId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBoxs",
                table: "OrderBoxs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxs_Boxes_BoxId",
                table: "OrderBoxs",
                column: "BoxId",
                principalTable: "Boxes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBoxs_Orders_OrderId",
                table: "OrderBoxs",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
