using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentiyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicApprovals");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "SellerApprovals");

            migrationBuilder.DropTable(
                name: "SitterApprovals");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "VerificationID",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsDefult",
                table: "AspNetRoles",
                newName: "IsDefault");

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproval",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDelete", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "220229cd-8d7f-4b6e-9411-4c8bf3df079c", "3cd7fbee-f947-4af1-be3f-1765dc49ced9", false, false, "Seller", "SELLER" },
                    { "8be5290b-5b0c-4e48-b6d4-5594d1495622", "8341db82-f2ae-480c-a55e-4604dba79030", false, false, "Clinic", "CLINIC" },
                    { "9cc3c82c-9f0a-44c6-ae43-f28b6c056ee5", "f9683f9e-57f3-4997-ac39-8ffa3b63b214", false, false, "Sitter", "SITTER" },
                    { "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a", "13ac201a-1f5b-4871-bb36-6912b4ed12bf", false, false, "Admin", "ADMIN" },
                    { "f3056a00-6914-4873-9f31-e3eb3ba6633a", "4e2c6469-ce64-4d4a-acb1-72ddabb6f0db", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsApproval", "LockoutEnabled", "LockoutEnd", "NationalID", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "a150d2df-c1c1-4317-954a-e39ce0db0682", 0, null, "7fa50e48-4080-4d01-8903-7c18c2e65392", "Admin@Petsica.com", true, false, false, null, null, "ADMIN@PETSICA.COM", "ADMIN@PETSICA.COM", "AQAAAAIAAYagAAAAEKRku5u6K325Irl1Utujiuil/WUhjTvShS9mJLXxO+2v/GKrMT1Ofhdp/0taFUO2bA==", null, false, null, "6B5AD15259FF426FBDC89A431C9541A2", false, null, "Admin@Petsica.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "pets:read", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 2, "permissions", "pets:add", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 3, "permissions", "pets:update", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 4, "permissions", "pets:delete", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 5, "permissions", "categories:read", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 6, "permissions", "categories:add", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 7, "permissions", "categories:update", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 8, "permissions", "users:read", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 9, "permissions", "users:add", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 10, "permissions", "users:update", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 11, "permissions", "roles:read", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 12, "permissions", "roles:add", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 13, "permissions", "roles:update", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 14, "permissions", "userFollows:read", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 15, "permissions", "userFollows:add", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 16, "permissions", "userFollows:update", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 17, "permissions", "likes:read", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 18, "permissions", "likes:add", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 19, "permissions", "likes:update", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 20, "permissions", "comments:read", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 21, "permissions", "comments:add", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 22, "permissions", "comments:update", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 23, "permissions", "posts:read", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 24, "permissions", "posts:add", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" },
                    { 25, "permissions", "posts:update", "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a", "a150d2df-c1c1-4317-954a-e39ce0db0682" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "220229cd-8d7f-4b6e-9411-4c8bf3df079c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8be5290b-5b0c-4e48-b6d4-5594d1495622");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cc3c82c-9f0a-44c6-ae43-f28b6c056ee5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3056a00-6914-4873-9f31-e3eb3ba6633a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a", "a150d2df-c1c1-4317-954a-e39ce0db0682" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a150d2df-c1c1-4317-954a-e39ce0db0682");

            migrationBuilder.DropColumn(
                name: "IsApproval",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "AspNetRoles",
                newName: "IsDefult");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Clinics",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Clinics",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VerificationID",
                table: "Clinics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicApprovals",
                columns: table => new
                {
                    ApprovalID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClinicID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicApprovals", x => x.ApprovalID);
                    table.ForeignKey(
                        name: "FK_ClinicApprovals_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "AdminID");
                    table.ForeignKey(
                        name: "FK_ClinicApprovals_Clinics_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinics",
                        principalColumn: "ClinicID");
                });

            migrationBuilder.CreateTable(
                name: "SellerApprovals",
                columns: table => new
                {
                    ApprovalID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerApprovals", x => x.ApprovalID);
                    table.ForeignKey(
                        name: "FK_SellerApprovals_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "AdminID");
                    table.ForeignKey(
                        name: "FK_SellerApprovals_Users_SellerID",
                        column: x => x.SellerID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "SitterApprovals",
                columns: table => new
                {
                    ApprovalID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SitterID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitterApprovals", x => x.ApprovalID);
                    table.ForeignKey(
                        name: "FK_SitterApprovals_Admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "Admins",
                        principalColumn: "AdminID");
                    table.ForeignKey(
                        name: "FK_SitterApprovals_Users_SitterID",
                        column: x => x.SitterID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicApprovals_AdminID",
                table: "ClinicApprovals",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicApprovals_ClinicID",
                table: "ClinicApprovals",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_SellerApprovals_AdminID",
                table: "SellerApprovals",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_SellerApprovals_SellerID",
                table: "SellerApprovals",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_SitterApprovals_AdminID",
                table: "SitterApprovals",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_SitterApprovals_SitterID",
                table: "SitterApprovals",
                column: "SitterID");
        }
    }
}
