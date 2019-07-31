using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InstagramBot.DB;
using InstagramBot.DB.Entities;
using InstagramBot.DB.Enums;
using InstagramBot.Service.Database;
using InstagramBot.Service.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InstagramBot.Service
{
    public class DbSetup
    {
        public async Task Run()
        {
            using (var db = new ApiContextFactory().CreateDbContext())
            {
                db.Database.Migrate();

                var adminUser = await db.Users.FirstOrDefaultAsync(x => x.Login == "Admin");
                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        Login = "Admin",
                        Password = "123456"
                    };
                    await db.Users.AddAsync(adminUser);
                    await db.SaveChangesAsync();
                }

                await CreateLikeInstaUserWithTasksIfNotExist(db, adminUser.Id, "1Travel_Around_The_World",
                    "Gfhjkm63934710", "Travel",
                    new List<string>()
                    {
                        "desartarch", "traveltherestoftheworld", "sardapp", "mygreekportfolio",
                        "europetraveldestinations", "living_europeofficial", "kayvanhuisseling", "tiffpenguin",
                        "choosemiami"
                    }, false);

                await CreateLikeInstaUserWithTasksIfNotExist(db, adminUser.Id, "Belarus.here", "Gfhjkm63934710",
                    "Minsk",
                    new List<string>()
                    {
                        "brestgood", "thismyminsk", "lovely_belarus", "ourbelarusby", "B0kedghoz67", "imagerb",
                        "bel.vovasha"
                    }, true);
            }
        }

        private static async Task CreateLikeInstaUserWithTasksIfNotExist(ApiContext db, long adminUserId, string login, string password, string tag, List<string> groupNames, bool isShowAuthor)
        {
            var instaUser1 =
                await db.InstagramUsers.FirstOrDefaultAsync(x => x.Login == login);
            if (instaUser1 == null)
            {
                instaUser1 = new InstagramUser
                {
                    Login = login,
                    Password = password,
                    AppUserId = adminUserId,
                    LoginStatus = LoginStatus.UnAuthenticated,
                    Created = DateTime.UtcNow,
                    QueueItems = new List<QueueItem>()
                    {
                        new QueueItem
                        {
                            QueueStatus = QueueStatus.InProgress,
                            QueueType = QueueType.Like,
                            Created = DateTime.UtcNow,
                            Modified = DateTime.UtcNow - TimeSpan.FromSeconds(100),
                            DelayInSeconds = 100,
                            Parameters = JsonConvert.SerializeObject(new LikeExecutorParameters {Tag = tag})
                        },
                        new QueueItem
                        {
                            QueueStatus = QueueStatus.InProgress,
                            QueueType = QueueType.PostMedia,
                            Created = DateTime.UtcNow,
                            Modified = DateTime.UtcNow - TimeSpan.FromSeconds(100),
                            DelayInSeconds = 7200,
                            Parameters = JsonConvert.SerializeObject(new PostMediaParameters {GroupNames = groupNames, IsShowAuthor = false})
                        }
                    }
                };
                await db.InstagramUsers.AddAsync(instaUser1);
                await db.SaveChangesAsync();
            }
        }
    }
}
