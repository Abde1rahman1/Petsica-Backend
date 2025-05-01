using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRequestPets");

            migrationBuilder.AddColumn<bool>(
                name: "Adoption",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Mating",
                table: "Pets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adoption",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Mating",
                table: "Pets");

            migrationBuilder.CreateTable(
                name: "UserRequestPets",
                columns: table => new
                {
                    PetID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Adoption = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Mating = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequestPets", x => new { x.PetID, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserRequestPets_Pets_PetID",
                        column: x => x.PetID,
                        principalTable: "Pets",
                        principalColumn: "PetID");
                    table.ForeignKey(
                        name: "FK_UserRequestPets_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRequestPets_UserID",
                table: "UserRequestPets",
                column: "UserID");
        }
    }
}
