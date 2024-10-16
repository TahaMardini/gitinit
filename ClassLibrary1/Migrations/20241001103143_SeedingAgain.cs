using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccessLayerDLL.Migrations
{
    /// <inheritdoc />
    public partial class SeedingAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fb0016c-8401-47f7-a3d7-26641f8881da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcf2591b-6c64-45b6-8e78-5a86a9a9c0ac");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e64c9749-e4eb-4d87-8bc8-406657a3398e", null, "User", "USER" },
                    { "f22017ce-b2d3-4565-b03c-ac4bb1124285", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e64c9749-e4eb-4d87-8bc8-406657a3398e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f22017ce-b2d3-4565-b03c-ac4bb1124285");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5fb0016c-8401-47f7-a3d7-26641f8881da", null, "User", "USER" },
                    { "bcf2591b-6c64-45b6-8e78-5a86a9a9c0ac", null, "Admin", "ADMIN" }
                });
        }
    }
}
