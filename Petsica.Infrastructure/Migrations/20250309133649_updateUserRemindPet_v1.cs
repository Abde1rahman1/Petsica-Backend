using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateUserRemindPet_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRemindPets",
                table: "UserRemindPets");

            migrationBuilder.AddColumn<int>(
                name: "UserRemindPetID",
                table: "UserRemindPets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRemindPets",
                table: "UserRemindPets",
                column: "UserRemindPetID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRemindPets_PetID",
                table: "UserRemindPets",
                column: "PetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRemindPets",
                table: "UserRemindPets");

            migrationBuilder.DropIndex(
                name: "IX_UserRemindPets_PetID",
                table: "UserRemindPets");

            migrationBuilder.DropColumn(
                name: "UserRemindPetID",
                table: "UserRemindPets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRemindPets",
                table: "UserRemindPets",
                columns: new[] { "PetID", "UserID" });
        }
    }
}
