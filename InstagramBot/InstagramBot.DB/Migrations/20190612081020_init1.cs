using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstagramBot.DB.Migrations
{
    public partial class init1 : Migration
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
                values: new object[] { 1L, 1L, null, new DateTime(2019, 6, 12, 8, 10, 20, 73, DateTimeKind.Utc).AddTicks(2996), "1Travel_Around_The_World", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gfhjkm63934710", "{\"DeviceInfo\":{\"PhoneGuid\":\"f73aad24-90be-49dd-babf-44faf9dacb9c\",\"DeviceGuid\":\"3a4fd2e7-a923-46f7-8466-6c9fb668e49d\",\"GoogleAdId\":\"7f611346-d7e8-4900-bb7a-3770d9ded7ae\",\"RankToken\":\"6050750a-3518-4957-88c6-c17b354b3f0e\",\"AdId\":\"af1c925e-36e9-41ae-934a-883d49762a3c\",\"AndroidVer\":{\"Codename\":\"Oreo\",\"VersionNumber\":\"8.1\",\"APILevel\":\"27\"},\"AndroidBoardName\":\"universal5420\",\"AndroidBootloader\":\"T705XXU1BOL2\",\"DeviceBrand\":\"samsung\",\"DeviceId\":\"android-ba3e1170e2a805a5\",\"DeviceModel\":\"Samsung Galaxy Tab S 8.4 LTE\",\"DeviceModelBoot\":\"universal5420\",\"DeviceModelIdentifier\":\"LRX22G.T705XXU1BOL2\",\"FirmwareBrand\":\"Samsung Galaxy Tab S 8.4 LTE\",\"FirmwareFingerprint\":\"samsung/klimtltexx/klimtlte:5.0.2/LRX22G/T705XXU1BOL2:user/release-keys\",\"FirmwareTags\":\"release-keys\",\"FirmwareType\":\"user\",\"HardwareManufacturer\":\"samsung\",\"HardwareModel\":\"SM-T705\",\"Resolution\":\"1440x2560\",\"Dpi\":\"640dpi\"},\"UserSession\":{\"UserName\":\"1Travel_Around_The_World\",\"Password\":\"Gfhjkm63934710\",\"LoggedInUser\":{\"IsVerified\":false,\"IsPrivate\":false,\"Pk\":8600514033,\"ProfilePicture\":\"https://scontent-frx5-1.cdninstagram.com/vp/6af2e79b388e72c0eccd7f5a14b8f86e/5D8466F6/t51.2885-19/s150x150/41706647_304043560383657_6801298339208888320_n.jpg?_nc_ht=scontent-frx5-1.cdninstagram.com\",\"ProfilePicUrl\":\"https://scontent-frx5-1.cdninstagram.com/vp/6af2e79b388e72c0eccd7f5a14b8f86e/5D8466F6/t51.2885-19/s150x150/41706647_304043560383657_6801298339208888320_n.jpg?_nc_ht=scontent-frx5-1.cdninstagram.com\",\"ProfilePictureId\":\"1871058647741605596_8600514033\",\"UserName\":\"1travel_around_the_world\",\"FullName\":\"Travel Around The World\"},\"RankToken\":\"8600514033_f73aad24-90be-49dd-babf-44faf9dacb9c\",\"CsrfToken\":\"RXG7Rxcxbq1ON0gjx6hOQcUbdC9VHjuK\",\"FacebookUserId\":\"\",\"FacebookAccessToken\":\"\"},\"IsAuthenticated\":true,\"Cookies\":{\"Capacity\":300,\"Count\":9,\"MaxCookieSize\":4096,\"PerDomainCapacity\":20},\"RawCookies\":[{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"0001-01-01T00:00:00\",\"Name\":\"rur\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:08.7478271+03:00\",\"Value\":\"FTW\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":false,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2029-06-09T10:14:07+03:00\",\"Name\":\"mid\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:15:51.6586832+03:00\",\"Value\":\"XQCmPwAEAAEqmPMpUrkTX3tMUwC1\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":false,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2020-06-10T10:16:07+03:00\",\"Name\":\"csrftoken\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:08.7478664+03:00\",\"Value\":\"9hFWuyKNi7yzhOcgCzEBde2PpZQ7r9Xd\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2019-09-10T10:16:02+03:00\",\"Name\":\"ds_user\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:04.2684317+03:00\",\"Value\":\"1travel_around_the_world\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2019-06-19T10:16:02+03:00\",\"Name\":\"shbid\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:04.2684707+03:00\",\"Value\":\"10514\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2019-06-19T10:16:02+03:00\",\"Name\":\"shbts\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:04.2684903+03:00\",\"Value\":\"1560323762.63188\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":false,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2019-09-10T10:16:07+03:00\",\"Name\":\"ds_user_id\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:08.747849+03:00\",\"Value\":\"8600514033\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"0001-01-01T00:00:00\",\"Name\":\"urlgen\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:08.7478634+03:00\",\"Value\":\"\\\"{\\\\\\\"46.53.188.198\\\\\\\": 20852}:1haxUN:WiBK2pvOmxfe4rZmrk9qmBAyzII\\\"\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2020-06-11T10:16:02+03:00\",\"Name\":\"sessionid\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:04.2685122+03:00\",\"Value\":\"8600514033%3AxCRHFjDTuImaZP%3A22\",\"Version\":0}],\"InstaApiVersion\":6}" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "QueueItems",
                columns: new[] { "Id", "Created", "DelayInSeconds", "InstagramUserId", "Modified", "Parameters", "QueueStatus", "QueueType" },
                values: new object[] { 1L, new DateTime(2019, 6, 12, 8, 10, 20, 73, DateTimeKind.Utc).AddTicks(8259), 100, 1L, new DateTime(2019, 6, 12, 8, 8, 40, 73, DateTimeKind.Utc).AddTicks(8270), "{\"Tag\":\"Travel\"}", 1, 0 });

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
