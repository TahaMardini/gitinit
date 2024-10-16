using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccessLayerDLL.Migrations
{
    /// <inheritdoc />
    public partial class AddingTemplateGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58c1a8e0-31d5-4119-8c79-0c24ba59d23e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cdebc50-ce12-4209-9ecf-282f584531ad");

            migrationBuilder.AlterColumn<string>(
                name: "TempName",
                table: "TaskListTemplates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TaskListTemplates",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "TemplateGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateGroups_TaskListTemplates_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "TaskListTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateGroups_TemplateID",
                table: "TemplateGroups",
                column: "TemplateID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateGroups");

            migrationBuilder.AlterColumn<string>(
                name: "TempName",
                table: "TaskListTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TaskListTemplates",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "58c1a8e0-31d5-4119-8c79-0c24ba59d23e", null, "User", "USER" },
                    { "8cdebc50-ce12-4209-9ecf-282f584531ad", null, "Admin", "ADMIN" }
                });
        }
    }
}
