using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petsica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class h : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "220229cd-8d7f-4b6e-9411-4c8bf3df079c",
                column: "Name",
                value: "SELLER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8be5290b-5b0c-4e48-b6d4-5594d1495622",
                column: "Name",
                value: "CLINIC");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cc3c82c-9f0a-44c6-ae43-f28b6c056ee5",
                column: "Name",
                value: "SITTER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a",
                column: "Name",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3056a00-6914-4873-9f31-e3eb3ba6633a",
                column: "Name",
                value: "MEMBER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "220229cd-8d7f-4b6e-9411-4c8bf3df079c",
                column: "Name",
                value: "Seller");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8be5290b-5b0c-4e48-b6d4-5594d1495622",
                column: "Name",
                value: "Clinic");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cc3c82c-9f0a-44c6-ae43-f28b6c056ee5",
                column: "Name",
                value: "Sitter");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a62e6ef4-d653-46dc-8cf8-cd9fa4512f4a",
                column: "Name",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3056a00-6914-4873-9f31-e3eb3ba6633a",
                column: "Name",
                value: "Member");
        }
    }
}
