using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace histopat_back.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToModuleAndSubtopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SubTopic",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SubTopic");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Modules");
        }
    }
}
