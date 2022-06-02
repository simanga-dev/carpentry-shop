using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpentryShop.Migrations
{
    public partial class _03e : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isComplete",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isComplete",
                table: "Boxes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isComplete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "isComplete",
                table: "Boxes");
        }
    }
}
