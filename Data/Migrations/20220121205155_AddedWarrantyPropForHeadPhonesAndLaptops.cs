using Microsoft.EntityFrameworkCore.Migrations;

namespace GamingStoreProject.Data.Migrations
{
    public partial class AddedWarrantyPropForHeadPhonesAndLaptops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Warranty",
                table: "Laptops",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Warranty",
                table: "Headphones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Warranty",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Warranty",
                table: "Headphones");
        }
    }
}
