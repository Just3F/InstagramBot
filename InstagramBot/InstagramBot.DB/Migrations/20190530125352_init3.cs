using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstagramBot.DB.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User2Roles_Role_RoleId",
                schema: "dbo",
                table: "User2Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "dbo",
                newName: "Roles",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "dbo",
                table: "Roles",
                column: "Id");

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
                    AccountStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramUsers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_User2Roles_Roles_RoleId",
                schema: "dbo",
                table: "User2Roles",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User2Roles_Roles_RoleId",
                schema: "dbo",
                table: "User2Roles");

            migrationBuilder.DropTable(
                name: "InstagramUsers",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "dbo",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "dbo",
                newName: "Role",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User2Roles_Role_RoleId",
                schema: "dbo",
                table: "User2Roles",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
