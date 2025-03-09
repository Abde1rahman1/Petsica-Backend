using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateMatingAdoption_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "UserRequestPets");

            migrationBuilder.AddColumn<bool>(
                name: "Adoption",
                table: "UserRequestPets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserRequestPets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Mating",
                table: "UserRequestPets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adoption",
                table: "UserRequestPets");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserRequestPets");

            migrationBuilder.DropColumn(
                name: "Mating",
                table: "UserRequestPets");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "UserRequestPets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
