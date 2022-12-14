using Microsoft.EntityFrameworkCore.Migrations;

namespace GamingStoreProject.Data.Migrations
{
    public partial class RemoveHD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HD",
                table: "Monitor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HD",
                table: "Monitor",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
