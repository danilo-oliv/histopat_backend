using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace histopat_back.Infra.Migrations
{
    /// <inheritdoc />
    public partial class TopicHistoryAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicHistory_Topic_TopicId",
                table: "TopicHistory");

            migrationBuilder.DropIndex(
                name: "IX_TopicHistory_TopicId",
                table: "TopicHistory");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "TopicHistory");

            migrationBuilder.CreateIndex(
                name: "IX_TopicHistory_IdTopic",
                table: "TopicHistory",
                column: "IdTopic");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicHistory_Topic_IdTopic",
                table: "TopicHistory",
                column: "IdTopic",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicHistory_Topic_IdTopic",
                table: "TopicHistory");

            migrationBuilder.DropIndex(
                name: "IX_TopicHistory_IdTopic",
                table: "TopicHistory");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "TopicHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TopicHistory_TopicId",
                table: "TopicHistory",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicHistory_Topic_TopicId",
                table: "TopicHistory",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
