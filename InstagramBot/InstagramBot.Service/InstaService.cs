﻿using System;
using System.Linq;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramBot.DB;
using InstagramBot.DB.Entities;
using InstagramBot.DB.Enums;
using InstagramBot.Service.Database;
using InstagramBot.Service.Executors;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InstagramBot.Service
{
    public class InstaService
    {
        private bool _isEnabled;

        public void Stop()
        {
            _isEnabled = false;
        }

        public async void Run()
        {
            Console.WriteLine("Instagram service is started.");
            _isEnabled = true;
            while (_isEnabled)
            {
                using (var db = new ApiContextFactory().CreateDbContext())
                {
                    await ActivateInstagramUsers(db);

                    var activeQueueItems = await db.QueueItems
                        .Include(x => x.InstagramUser)
                        .Where(x => x.QueueStatus == QueueStatus.InProgress &&
                                    x.InstagramUser.LoginStatus == LoginStatus.Authenticated &&
                                    x.Modified < DateTime.UtcNow - TimeSpan.FromSeconds(x.DelayInSeconds))
                        .ToListAsync();

                    foreach (var queueItem in activeQueueItems)
                    {
                        var instaUser = queueItem.InstagramUser;
                        var instaFactory = new InstaFactory();
                        var instaApi = await instaFactory.BuildInstaApi(instaUser.Login, instaUser.Password);
                        if (instaApi == null)
                        {
                            queueItem.QueueStatus = QueueStatus.Stopped;
                            break;
                        }

                        var executor = GetBaseExecutor(queueItem, instaApi, db);
                        var result = await executor.Run(queueItem);
                        Console.WriteLine(result.Message + "for instagram user: " + instaUser.Login + " at " + DateTime.Now.ToString("T"));
                    }
                }
            }

            Console.WriteLine("Instagram service is stopped.");
        }


        private static async Task ActivateInstagramUsers(ApiContext db)
        {
            var usersForActivate = await db.InstagramUsers
                .Where(x => x.QueueItems.Select(z => z.QueueStatus).Contains(QueueStatus.InProgress) &&
                            ((string.IsNullOrEmpty(x.Session) &&
                              x.LoginStatus != LoginStatus.WaitForCheckChallengeRequiredCode &&
                              x.LoginStatus != LoginStatus.Authenticated) ||
                             (x.LoginStatus == LoginStatus.WaitForCheckChallengeRequiredCode &&
                              !string.IsNullOrEmpty(x.ChallengeRequiredCode))))
                .ToListAsync();

            foreach (var user in usersForActivate)
            {
                var instaFactory = new InstaFactory();
                await instaFactory.SetSession(user);
                await db.SaveChangesAsync();
            }
        }

        private IBaseExecutor GetBaseExecutor(QueueItem queueItem, IInstaApi instaApi, ApiContext db)
        {
            IBaseExecutor executor;
            switch (queueItem.QueueType)
            {
                case QueueType.Like:
                    executor = new LikeExecutor(instaApi, db);
                    break;
                case QueueType.PostMedia:
                    executor = new PostMediaExecutor(instaApi, db);
                    break;
                default:
                    executor = new LikeExecutor(instaApi, db);
                    break;
            }

            return executor;
        }
    }
}
