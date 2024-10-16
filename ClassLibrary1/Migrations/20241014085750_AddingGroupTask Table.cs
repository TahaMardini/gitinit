using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessLayerDLL.Migrations
{
    /// <inheritdoc />
    public partial class AddingGroupTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "TemplateGroups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TempName",
                table: "TaskListTemplates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GroupTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TaskName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    DependancyTaskID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupTasks_GroupTasks_DependancyTaskID",
                        column: x => x.DependancyTaskID,
                        principalTable: "GroupTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupTasks_TemplateGroups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "TemplateGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupTasks_DependancyTaskID",
                table: "GroupTasks",
                column: "DependancyTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTasks_GroupID",
                table: "GroupTasks",
                column: "GroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupTasks");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "TemplateGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TempName",
                table: "TaskListTemplates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
