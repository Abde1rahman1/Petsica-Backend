using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateUserRemindPet_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UserRemindPets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "UserRemindPets");
        }
    }
}
