using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstagramBot.DB.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AppUserId",
                schema: "dbo",
                table: "InstagramUsers",
                nullable: false,
                defaultValue: 0L);

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
                    QueueType = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "InstagramUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "AppUserId",
                value: 1L);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "QueueItems",
                columns: new[] { "Id", "Created", "InstagramUserId", "Modified", "Parameters", "QueueStatus" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_InstagramUsers_AppUserId",
                schema: "dbo",
                table: "InstagramUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueItems_InstagramUserId",
                schema: "dbo",
                table: "QueueItems",
                column: "InstagramUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstagramUsers_Users_AppUserId",
                schema: "dbo",
                table: "InstagramUsers",
                column: "AppUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstagramUsers_Users_AppUserId",
                schema: "dbo",
                table: "InstagramUsers");

            migrationBuilder.DropTable(
                name: "QueueItems",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_InstagramUsers_AppUserId",
                schema: "dbo",
                table: "InstagramUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                schema: "dbo",
                table: "InstagramUsers");
        }
    }
}
