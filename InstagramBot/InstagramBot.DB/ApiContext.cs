using System;
using System.Linq;
using System.Threading.Tasks;
using InstagramBot.DB.Entities;
using InstagramBot.DB.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InstagramBot.DB
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public DbSet<QueueHistory> QueueHistories { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InstagramUser> InstagramUsers { get; set; }
        public DbSet<QueueItem> QueueItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema: DbGlobals.SchemaName);

            FillData(modelBuilder);

            modelBuilder.ApplyConfiguration(new User2RolesEntityConfiguration());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        private static void FillData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(new AppUser { Id = 1, Login = "Admin", Password = "123456", });
            modelBuilder.Entity<InstagramUser>().HasData(new InstagramUser
            {
                Id = 1,
                Login = "1Travel_Around_The_World",
                Password = "Gfhjkm63934710",
                AppUserId = 1,
                LoginStatus = LoginStatus.Authenticated,
                Created = DateTime.UtcNow,
                Session = "{\"DeviceInfo\":{\"PhoneGuid\":\"f73aad24-90be-49dd-babf-44faf9dacb9c\",\"DeviceGuid\":\"3a4fd2e7-a923-46f7-8466-6c9fb668e49d\",\"GoogleAdId\":\"7f611346-d7e8-4900-bb7a-3770d9ded7ae\",\"RankToken\":\"6050750a-3518-4957-88c6-c17b354b3f0e\",\"AdId\":\"af1c925e-36e9-41ae-934a-883d49762a3c\",\"AndroidVer\":{\"Codename\":\"Oreo\",\"VersionNumber\":\"8.1\",\"APILevel\":\"27\"},\"AndroidBoardName\":\"universal5420\",\"AndroidBootloader\":\"T705XXU1BOL2\",\"DeviceBrand\":\"samsung\",\"DeviceId\":\"android-ba3e1170e2a805a5\",\"DeviceModel\":\"Samsung Galaxy Tab S 8.4 LTE\",\"DeviceModelBoot\":\"universal5420\",\"DeviceModelIdentifier\":\"LRX22G.T705XXU1BOL2\",\"FirmwareBrand\":\"Samsung Galaxy Tab S 8.4 LTE\",\"FirmwareFingerprint\":\"samsung/klimtltexx/klimtlte:5.0.2/LRX22G/T705XXU1BOL2:user/release-keys\",\"FirmwareTags\":\"release-keys\",\"FirmwareType\":\"user\",\"HardwareManufacturer\":\"samsung\",\"HardwareModel\":\"SM-T705\",\"Resolution\":\"1440x2560\",\"Dpi\":\"640dpi\"},\"UserSession\":{\"UserName\":\"1Travel_Around_The_World\",\"Password\":\"Gfhjkm63934710\",\"LoggedInUser\":{\"IsVerified\":false,\"IsPrivate\":false,\"Pk\":8600514033,\"ProfilePicture\":\"https://scontent-frx5-1.cdninstagram.com/vp/6af2e79b388e72c0eccd7f5a14b8f86e/5D8466F6/t51.2885-19/s150x150/41706647_304043560383657_6801298339208888320_n.jpg?_nc_ht=scontent-frx5-1.cdninstagram.com\",\"ProfilePicUrl\":\"https://scontent-frx5-1.cdninstagram.com/vp/6af2e79b388e72c0eccd7f5a14b8f86e/5D8466F6/t51.2885-19/s150x150/41706647_304043560383657_6801298339208888320_n.jpg?_nc_ht=scontent-frx5-1.cdninstagram.com\",\"ProfilePictureId\":\"1871058647741605596_8600514033\",\"UserName\":\"1travel_around_the_world\",\"FullName\":\"Travel Around The World\"},\"RankToken\":\"8600514033_f73aad24-90be-49dd-babf-44faf9dacb9c\",\"CsrfToken\":\"RXG7Rxcxbq1ON0gjx6hOQcUbdC9VHjuK\",\"FacebookUserId\":\"\",\"FacebookAccessToken\":\"\"},\"IsAuthenticated\":true,\"Cookies\":{\"Capacity\":300,\"Count\":9,\"MaxCookieSize\":4096,\"PerDomainCapacity\":20},\"RawCookies\":[{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"0001-01-01T00:00:00\",\"Name\":\"rur\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:08.7478271+03:00\",\"Value\":\"FTW\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":false,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2029-06-09T10:14:07+03:00\",\"Name\":\"mid\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:15:51.6586832+03:00\",\"Value\":\"XQCmPwAEAAEqmPMpUrkTX3tMUwC1\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":false,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2020-06-10T10:16:07+03:00\",\"Name\":\"csrftoken\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:08.7478664+03:00\",\"Value\":\"9hFWuyKNi7yzhOcgCzEBde2PpZQ7r9Xd\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2019-09-10T10:16:02+03:00\",\"Name\":\"ds_user\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:04.2684317+03:00\",\"Value\":\"1travel_around_the_world\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2019-06-19T10:16:02+03:00\",\"Name\":\"shbid\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:04.2684707+03:00\",\"Value\":\"10514\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2019-06-19T10:16:02+03:00\",\"Name\":\"shbts\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:04.2684903+03:00\",\"Value\":\"1560323762.63188\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":false,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2019-09-10T10:16:07+03:00\",\"Name\":\"ds_user_id\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:08.747849+03:00\",\"Value\":\"8600514033\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"0001-01-01T00:00:00\",\"Name\":\"urlgen\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:08.7478634+03:00\",\"Value\":\"\\\"{\\\\\\\"46.53.188.198\\\\\\\": 20852}:1haxUN:WiBK2pvOmxfe4rZmrk9qmBAyzII\\\"\",\"Version\":0},{\"Comment\":\"\",\"CommentUri\":null,\"HttpOnly\":true,\"Discard\":false,\"Domain\":\".instagram.com\",\"Expired\":false,\"Expires\":\"2020-06-11T10:16:02+03:00\",\"Name\":\"sessionid\",\"Path\":\"/\",\"Port\":\"\",\"Secure\":true,\"TimeStamp\":\"2019-06-12T10:16:04.2685122+03:00\",\"Value\":\"8600514033%3AxCRHFjDTuImaZP%3A22\",\"Version\":0}],\"InstaApiVersion\":6}"

            });
            modelBuilder.Entity<QueueItem>().HasData(new QueueItem
            {
                Id = 1,
                InstagramUserId = 1,
                QueueStatus = QueueStatus.InProgress,
                QueueType = QueueType.Like,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow - TimeSpan.FromSeconds(100),
                DelayInSeconds = 100,
                Parameters = JsonConvert.SerializeObject(new LikeExecutorParameters { Tag = "Travel" })
            });
        }

        private class LikeExecutorParameters
        {
            public string Tag { get; set; }
        }

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuditInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is AuditEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AuditEntity)entry.Entity).Created = DateTime.UtcNow;
                }
                ((AuditEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
