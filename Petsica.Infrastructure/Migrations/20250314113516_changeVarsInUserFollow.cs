using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeVarsInUserFollow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_Users_FollowerUserId",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "FollowerUserId",
                table: "UserFollows",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_FollowerUserId",
                table: "UserFollows",
                newName: "IX_UserFollows_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_Users_UserId",
                table: "UserFollows",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_Users_UserId",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserFollows",
                newName: "FollowerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_UserId",
                table: "UserFollows",
                newName: "IX_UserFollows_FollowerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_Users_FollowerUserId",
                table: "UserFollows",
                column: "FollowerUserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
