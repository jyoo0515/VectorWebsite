using Microsoft.EntityFrameworkCore.Migrations;

namespace VectorWebsite.Persistance.Migrations
{
    public partial class CommentReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Report",
                table: "PetitionComments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Report",
                table: "PetitionComments");
        }
    }
}
