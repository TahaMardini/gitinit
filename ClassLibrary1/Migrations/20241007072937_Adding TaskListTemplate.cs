using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccessLayerDLL.Migrations
{
    /// <inheritdoc />
    public partial class AddingTaskListTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18fa1631-81da-4b8e-864e-9d22b2b43358");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "439d408d-b856-4f4e-90e3-6175995c7b5f");

            migrationBuilder.CreateTable(
                name: "TaskListTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TempName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListTemplates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58c1a8e0-31d5-4119-8c79-0c24ba59d23e", null, "User", "USER" },
                    { "8cdebc50-ce12-4209-9ecf-282f584531ad", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskListTemplates");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58c1a8e0-31d5-4119-8c79-0c24ba59d23e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cdebc50-ce12-4209-9ecf-282f584531ad");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18fa1631-81da-4b8e-864e-9d22b2b43358", null, "User", "USER" },
                    { "439d408d-b856-4f4e-90e3-6175995c7b5f", null, "Admin", "ADMIN" }
                });
        }
    }
}
