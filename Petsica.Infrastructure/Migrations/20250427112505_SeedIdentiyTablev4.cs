using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentiyTablev4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDelete", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "220229cd-8d7f-4b6e-9411-4c8bf3df079c", "3cd7fbee-f947-4af1-be3f-1765dc49ced9", false, false, "SELLER", "SELLER" },
                    { "8be5290b-5b0c-4e48-b6d4-5594d1495622", "8341db82-f2ae-480c-a55e-4604dba79030", false, false, "CLINIC", "CLINIC" },
                    { "9cc3c82c-9f0a-44c6-ae43-f28b6c056ee5", "f9683f9e-57f3-4997-ac39-8ffa3b63b214", false, false, "SITTER", "SITTER" },
                    { "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a", "13ac201a-1f5b-4871-bb36-6912b4ed12bf", false, false, "ADMIN", "ADMIN" },
                    { "f3056a00-6914-4873-9f31-e3eb3ba6633a", "4e2c6469-ce64-4d4a-acb1-72ddabb6f0db", true, false, "MEMBER", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ApprovalPhoto", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsApproval", "IsDisabled", "LockoutEnabled", "LockoutEnd", "NationalID", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "a150d2df-c1c1-4317-954a-e39ce0db0682", 0, null, null, "7fa50e48-4080-4d01-8903-7c18c2e65392", "Admin@Petsica.com", true, false, false, false, null, null, "ADMIN@PETSICA.COM", "ADMIN@PETSICA.COM", "AQAAAAIAAYagAAAAEKRku5u6K325Irl1Utujiuil/WUhjTvShS9mJLXxO+2v/GKrMT1Ofhdp/0taFUO2bA==", null, false, null, "6B5AD15259FF426FBDC89A431C9541A2", false, null, "Admin@Petsica.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a", "a150d2df-c1c1-4317-954a-e39ce0db0682" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
