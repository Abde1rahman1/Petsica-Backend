using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_SellerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Users_UserID",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SellerID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_SitterID",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentPosts_Users_UserID",
                table: "UserCommentPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_Users_FollowedUserId",
                table: "UserFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_Users_UserId",
                table: "UserFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikePosts_Users_UserID",
                table: "UserLikePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessageClinics_Users_UserID",
                table: "UserMessageClinics");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessageUsers_Users_UserReceiverID",
                table: "UserMessageUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessageUsers_Users_UserSenderID",
                table: "UserMessageUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRemindPets_Users_UserID",
                table: "UserRemindPets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRequestServices_Users_UserID",
                table: "UserRequestServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_User_UserID",
                table: "Carts",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_SellerID",
                table: "Orders",
                column: "SellerID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_UserID",
                table: "Orders",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_User_UserID",
                table: "Pets",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_User_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_User_SellerID",
                table: "Products",
                column: "SellerID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_User_SitterID",
                table: "Services",
                column: "SitterID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentPosts_User_UserID",
                table: "UserCommentPosts",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_User_FollowedUserId",
                table: "UserFollows",
                column: "FollowedUserId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_User_UserId",
                table: "UserFollows",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikePosts_User_UserID",
                table: "UserLikePosts",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessageClinics_User_UserID",
                table: "UserMessageClinics",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessageUsers_User_UserReceiverID",
                table: "UserMessageUsers",
                column: "UserReceiverID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessageUsers_User_UserSenderID",
                table: "UserMessageUsers",
                column: "UserSenderID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRemindPets_User_UserID",
                table: "UserRemindPets",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRequestServices_User_UserID",
                table: "UserRequestServices",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_User_UserID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_SellerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_UserID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_User_UserID",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_User_UserID",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_User_SellerID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_User_SitterID",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentPosts_User_UserID",
                table: "UserCommentPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_User_FollowedUserId",
                table: "UserFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_User_UserId",
                table: "UserFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikePosts_User_UserID",
                table: "UserLikePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessageClinics_User_UserID",
                table: "UserMessageClinics");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessageUsers_User_UserReceiverID",
                table: "UserMessageUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessageUsers_User_UserSenderID",
                table: "UserMessageUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRemindPets_User_UserID",
                table: "UserRemindPets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRequestServices_User_UserID",
                table: "UserRequestServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserID",
                table: "Carts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_SellerID",
                table: "Orders",
                column: "SellerID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Users_UserID",
                table: "Pets",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SellerID",
                table: "Products",
                column: "SellerID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_SitterID",
                table: "Services",
                column: "SitterID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentPosts_Users_UserID",
                table: "UserCommentPosts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_Users_FollowedUserId",
                table: "UserFollows",
                column: "FollowedUserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_Users_UserId",
                table: "UserFollows",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikePosts_Users_UserID",
                table: "UserLikePosts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessageClinics_Users_UserID",
                table: "UserMessageClinics",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessageUsers_Users_UserReceiverID",
                table: "UserMessageUsers",
                column: "UserReceiverID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessageUsers_Users_UserSenderID",
                table: "UserMessageUsers",
                column: "UserSenderID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRemindPets_Users_UserID",
                table: "UserRemindPets",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRequestServices_Users_UserID",
                table: "UserRequestServices",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
