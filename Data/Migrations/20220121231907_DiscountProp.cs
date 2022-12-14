using Microsoft.EntityFrameworkCore.Migrations;

namespace GamingStoreProject.Data.Migrations
{
    public partial class DiscountProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "PCs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Mouses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "MousePads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Monitor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Laptops",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Keyboards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Headphones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "GraphicCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "CPUs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "PCs");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Mouses");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "MousePads");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Monitor");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Headphones");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "GraphicCards");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CPUs");
        }
    }
}
