using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentiyTablev5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
