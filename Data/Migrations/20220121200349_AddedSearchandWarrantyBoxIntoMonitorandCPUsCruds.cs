using Microsoft.EntityFrameworkCore.Migrations;

namespace GamingStoreProject.Data.Migrations
{
    public partial class AddedSearchandWarrantyBoxIntoMonitorandCPUsCruds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Warranty",
                table: "CPUs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Warranty",
                table: "CPUs");
        }
    }
}
