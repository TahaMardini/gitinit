using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccessLayerDLL.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingProgramCs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "18fa1631-81da-4b8e-864e-9d22b2b43358", null, "User", "USER" },
                    { "439d408d-b856-4f4e-90e3-6175995c7b5f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18fa1631-81da-4b8e-864e-9d22b2b43358");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "439d408d-b856-4f4e-90e3-6175995c7b5f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e64c9749-e4eb-4d87-8bc8-406657a3398e", null, "User", "USER" },
                    { "f22017ce-b2d3-4565-b03c-ac4bb1124285", null, "Admin", "ADMIN" }
                });
        }
    }
}
