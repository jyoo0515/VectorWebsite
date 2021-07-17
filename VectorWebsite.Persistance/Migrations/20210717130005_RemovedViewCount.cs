using Microsoft.EntityFrameworkCore.Migrations;

namespace VectorWebsite.Persistance.Migrations
{
    public partial class RemovedViewCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickNum",
                table: "Notices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClickNum",
                table: "Notices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
