using Microsoft.EntityFrameworkCore.Migrations;

namespace VectorWebsite.Persistance.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Petitions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PetitionComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Notices",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Inquiries",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Petitions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "PetitionComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Notices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Inquiries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Petitions_CreatorId",
                table: "Petitions",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PetitionComments_CreatorId",
                table: "PetitionComments",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notices_CreatorId",
                table: "Notices",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CreatorId",
                table: "Inquiries",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_AspNetUsers_CreatorId",
                table: "Inquiries",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_AspNetUsers_CreatorId",
                table: "Notices",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PetitionComments_AspNetUsers_CreatorId",
                table: "PetitionComments",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Petitions_AspNetUsers_CreatorId",
                table: "Petitions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_AspNetUsers_CreatorId",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_AspNetUsers_CreatorId",
                table: "Notices");

            migrationBuilder.DropForeignKey(
                name: "FK_PetitionComments_AspNetUsers_CreatorId",
                table: "PetitionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Petitions_AspNetUsers_CreatorId",
                table: "Petitions");

            migrationBuilder.DropIndex(
                name: "IX_Petitions_CreatorId",
                table: "Petitions");

            migrationBuilder.DropIndex(
                name: "IX_PetitionComments_CreatorId",
                table: "PetitionComments");

            migrationBuilder.DropIndex(
                name: "IX_Notices_CreatorId",
                table: "Notices");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_CreatorId",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Petitions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "PetitionComments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Notices",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Inquiries",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Petitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PetitionComments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Inquiries",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
