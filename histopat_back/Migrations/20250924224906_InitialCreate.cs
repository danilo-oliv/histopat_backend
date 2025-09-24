using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace histopat_back.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    IdModule = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastModified = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.IdModule);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRoles = table.Column<byte>(type: "TINYINT", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRoles);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    IdTopico = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdModule = table.Column<long>(type: "BIGINT", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastModified = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.IdTopico);
                    table.ForeignKey(
                        name: "FK_Topic_Module_IdModule",
                        column: x => x.IdModule,
                        principalTable: "Module",
                        principalColumn: "IdModule",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleHistory",
                columns: table => new
                {
                    IdModuleHistory = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModificationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    SnapshotData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUser = table.Column<long>(type: "BIGINT", nullable: false),
                    IdModule = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleHistory", x => x.IdModuleHistory);
                    table.ForeignKey(
                        name: "FK_ModuleHistory_Module_IdModule",
                        column: x => x.IdModule,
                        principalTable: "Module",
                        principalColumn: "IdModule",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleHistory_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "BIGINT", nullable: false),
                    IdRoles = table.Column<byte>(type: "TINYINT", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.IdUser, x.IdRoles });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_IdRoles",
                        column: x => x.IdRoles,
                        principalTable: "Roles",
                        principalColumn: "IdRoles",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubTopic",
                columns: table => new
                {
                    IdSubTopico = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTopic = table.Column<long>(type: "BIGINT", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastModified = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTopic", x => x.IdSubTopico);
                    table.ForeignKey(
                        name: "FK_SubTopic_Topic_IdTopic",
                        column: x => x.IdTopic,
                        principalTable: "Topic",
                        principalColumn: "IdTopico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicHistory",
                columns: table => new
                {
                    IdTopicHistory = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModificationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    SnapshotData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUser = table.Column<long>(type: "BIGINT", nullable: false),
                    IdTopico = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicHistory", x => x.IdTopicHistory);
                    table.ForeignKey(
                        name: "FK_TopicHistory_Topic_IdTopico",
                        column: x => x.IdTopico,
                        principalTable: "Topic",
                        principalColumn: "IdTopico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicHistory_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Slide",
                columns: table => new
                {
                    IdSlide = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSubTopico = table.Column<long>(type: "BIGINT", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastModified = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slide", x => x.IdSlide);
                    table.ForeignKey(
                        name: "FK_Slide_SubTopic_IdSubTopico",
                        column: x => x.IdSubTopico,
                        principalTable: "SubTopic",
                        principalColumn: "IdSubTopico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubTopicHistory",
                columns: table => new
                {
                    IdSubTopicHistory = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModificationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    SnapshotData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSubTopico = table.Column<long>(type: "BIGINT", nullable: false),
                    IdUser = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTopicHistory", x => x.IdSubTopicHistory);
                    table.ForeignKey(
                        name: "FK_SubTopicHistory_SubTopic_IdSubTopico",
                        column: x => x.IdSubTopico,
                        principalTable: "SubTopic",
                        principalColumn: "IdSubTopico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubTopicHistory_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlideHistory",
                columns: table => new
                {
                    IdSlideSubTopicHistory = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModificationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    SnapshotData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSlide = table.Column<long>(type: "BIGINT", nullable: false),
                    IdUser = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideHistory", x => x.IdSlideSubTopicHistory);
                    table.ForeignKey(
                        name: "FK_SlideHistory_Slide_IdSlide",
                        column: x => x.IdSlide,
                        principalTable: "Slide",
                        principalColumn: "IdSlide",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SlideHistory_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleHistory_IdModule",
                table: "ModuleHistory",
                column: "IdModule");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleHistory_IdUser",
                table: "ModuleHistory",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Slide_IdSubTopico",
                table: "Slide",
                column: "IdSubTopico");

            migrationBuilder.CreateIndex(
                name: "IX_SlideHistory_IdSlide",
                table: "SlideHistory",
                column: "IdSlide");

            migrationBuilder.CreateIndex(
                name: "IX_SlideHistory_IdUser",
                table: "SlideHistory",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_SubTopic_IdTopic",
                table: "SubTopic",
                column: "IdTopic");

            migrationBuilder.CreateIndex(
                name: "IX_SubTopicHistory_IdSubTopico",
                table: "SubTopicHistory",
                column: "IdSubTopico");

            migrationBuilder.CreateIndex(
                name: "IX_SubTopicHistory_IdUser",
                table: "SubTopicHistory",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_IdModule",
                table: "Topic",
                column: "IdModule");

            migrationBuilder.CreateIndex(
                name: "IX_TopicHistory_IdTopico",
                table: "TopicHistory",
                column: "IdTopico");

            migrationBuilder.CreateIndex(
                name: "IX_TopicHistory_IdUser",
                table: "TopicHistory",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_IdRoles",
                table: "UserRole",
                column: "IdRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleHistory");

            migrationBuilder.DropTable(
                name: "SlideHistory");

            migrationBuilder.DropTable(
                name: "SubTopicHistory");

            migrationBuilder.DropTable(
                name: "TopicHistory");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Slide");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SubTopic");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "Module");
        }
    }
}
