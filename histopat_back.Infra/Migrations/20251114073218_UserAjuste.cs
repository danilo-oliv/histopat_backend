using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace histopat_back.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UserAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_IdRole",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_IdUser",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_SlideHistory_User_IdUser",
                table: "SlideHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTopicHistory_User_IdUser",
                table: "SubTopicHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicHistory_User_IdUser",
                table: "TopicHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleHistory_User_IdUser",
                table: "ModuleHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "IdRole",
                table: "Role");

            migrationBuilder.AddColumn<byte>(
                name: "IdRole",
                table: "Role",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 1)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte>(
                name: "IdRole",
                table: "UserRole",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "IdUser",
                table: "UserRole",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "IdRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "IdUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "IdUser", "IdRole" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_IdRole",
                table: "UserRole",
                column: "IdRole",
                principalTable: "Role",
                principalColumn: "IdRole",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_IdUser",
                table: "UserRole",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SlideHistory_User_IdUser",
                table: "SlideHistory",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTopicHistory_User_IdUser",
                table: "SubTopicHistory",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicHistory_User_IdUser",
                table: "TopicHistory",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleHistory_User_IdUser",
                table: "ModuleHistory",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_IdRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.AlterColumn<byte>(
                name: "IdRole",
                table: "UserRole",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdUserRole",
                table: "UserRole",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<byte>(
                name: "IdRole",
                table: "Role",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "IdUserRole");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_IdUser",
                table: "UserRole",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_IdRole",
                table: "UserRole",
                column: "IdRole",
                principalTable: "Role",
                principalColumn: "IdRole",
                onDelete: ReferentialAction.Cascade);
        }
        
    }
}
