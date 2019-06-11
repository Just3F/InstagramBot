using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstagramBot.DB.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstagramUsers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Session = table.Column<string>(nullable: true),
                    LoginStatus = table.Column<int>(nullable: false),
                    ChallengeRequiredCode = table.Column<string>(nullable: true),
                    AppUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstagramUsers_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User2Roles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User2Roles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_User2Roles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User2Roles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QueueItems",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Parameters = table.Column<string>(nullable: true),
                    QueueStatus = table.Column<int>(nullable: false),
                    QueueType = table.Column<int>(nullable: false),
                    DelayInSeconds = table.Column<int>(nullable: false),
                    InstagramUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueueItems_InstagramUsers_InstagramUserId",
                        column: x => x.InstagramUserId,
                        principalSchema: "dbo",
                        principalTable: "InstagramUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QueueHistories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OldQueueStatus = table.Column<int>(nullable: false),
                    NewQueueStatus = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    QueueItemId = table.Column<long>(nullable: false),
                    CreatedById = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueueHistories_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QueueHistories_QueueItems_QueueItemId",
                        column: x => x.QueueItemId,
                        principalSchema: "dbo",
                        principalTable: "QueueItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "Created", "Login", "Modified", "Password" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123456" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "InstagramUsers",
                columns: new[] { "Id", "AppUserId", "ChallengeRequiredCode", "Created", "Login", "LoginStatus", "Modified", "Name", "Password", "Session" },
                values: new object[] { 1L, 1L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "belarus.here", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gfhjkm63934710", null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "QueueItems",
                columns: new[] { "Id", "Created", "DelayInSeconds", "InstagramUserId", "Modified", "Parameters", "QueueStatus", "QueueType" },
                values: new object[] { 1L, new DateTime(2019, 6, 11, 10, 38, 19, 945, DateTimeKind.Utc).AddTicks(4794), 100, 1L, new DateTime(2019, 6, 11, 10, 38, 19, 945, DateTimeKind.Utc).AddTicks(5376), "{\"Tag\":\"Minsk\"}", 1, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_InstagramUsers_AppUserId",
                schema: "dbo",
                table: "InstagramUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueHistories_CreatedById",
                schema: "dbo",
                table: "QueueHistories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_QueueHistories_QueueItemId",
                schema: "dbo",
                table: "QueueHistories",
                column: "QueueItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueItems_InstagramUserId",
                schema: "dbo",
                table: "QueueItems",
                column: "InstagramUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User2Roles_UserId",
                schema: "dbo",
                table: "User2Roles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueueHistories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User2Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "QueueItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InstagramUsers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
