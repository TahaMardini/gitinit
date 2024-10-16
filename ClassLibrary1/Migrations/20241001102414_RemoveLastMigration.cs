using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccessLayerDLL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e108c79-8a6b-45fb-b224-a415349c1875");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5eb9f00-87d3-4614-969b-b2c284be4716");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5fb0016c-8401-47f7-a3d7-26641f8881da", null, "User", "USER" },
                    { "bcf2591b-6c64-45b6-8e78-5a86a9a9c0ac", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "4e108c79-8a6b-45fb-b224-a415349c1875", null, "Admin", "ADMIN" },
                    { "b5eb9f00-87d3-4614-969b-b2c284be4716", null, "User", "USER" }
                });
        }
    }
}
