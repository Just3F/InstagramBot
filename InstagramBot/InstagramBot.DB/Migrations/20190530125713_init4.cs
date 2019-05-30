using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstagramBot.DB.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "InstagramUsers",
                columns: new[] { "Id", "AccountStatus", "Created", "Login", "Modified", "Name", "Password", "Session" },
                values: new object[] { 1L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "belarus.here", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gfhjkm63934710", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "InstagramUsers",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
