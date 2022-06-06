using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarpentryShop.Migrations
{
    public partial class sweet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "isComplete",
                table: "OrderBoxes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isComplete",
                table: "OrderBoxes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }
    }
}
