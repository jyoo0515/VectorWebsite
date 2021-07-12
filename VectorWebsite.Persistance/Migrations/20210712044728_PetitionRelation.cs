using Microsoft.EntityFrameworkCore.Migrations;

namespace VectorWebsite.Persistance.Migrations
{
    public partial class PetitionRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetitionComments_Petitions_PetitionId",
                table: "PetitionComments");

            migrationBuilder.AddForeignKey(
                name: "FK_PetitionComments_Petitions_PetitionId",
                table: "PetitionComments",
                column: "PetitionId",
                principalTable: "Petitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetitionComments_Petitions_PetitionId",
                table: "PetitionComments");

            migrationBuilder.AddForeignKey(
                name: "FK_PetitionComments_Petitions_PetitionId",
                table: "PetitionComments",
                column: "PetitionId",
                principalTable: "Petitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
