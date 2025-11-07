using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace histopat_back.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ModuleHistoryAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleHistory_Module_ModuleId",
                table: "ModuleHistory");

            migrationBuilder.DropIndex(
                name: "IX_ModuleHistory_ModuleId",
                table: "ModuleHistory");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "ModuleHistory");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleHistory_IdModule",
                table: "ModuleHistory",
                column: "IdModule");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleHistory_Module_IdModule",
                table: "ModuleHistory",
                column: "IdModule",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleHistory_Module_IdModule",
                table: "ModuleHistory");

            migrationBuilder.DropIndex(
                name: "IX_ModuleHistory_IdModule",
                table: "ModuleHistory");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "ModuleHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleHistory_ModuleId",
                table: "ModuleHistory",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleHistory_Module_ModuleId",
                table: "ModuleHistory",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
