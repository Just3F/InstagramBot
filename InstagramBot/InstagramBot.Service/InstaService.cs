using System;
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
                    var usersForActivate = await db.InstagramUsers
                        .Where(x => x.QueueItems.Select(z => z.QueueStatus).Contains(QueueStatus.InProgress) &&
                                    (string.IsNullOrEmpty(x.Session) || x.LoginStatus != LoginStatus.Authenticated))
                        .ToListAsync();

                    foreach (var user in usersForActivate)
                    {
                        user.
                    }

                    var queueItems = await db.QueueItems
                        .Where(x => x.QueueStatus == QueueStatus.InProgress &&
                                    x.Modified < DateTime.UtcNow - TimeSpan.FromSeconds(x.DelayInSeconds))
                        .Include(x => x.InstagramUser).ToListAsync();

                    foreach (var queueItem in queueItems)
                    {
                        var instaUser = queueItem.InstagramUser;
                        var instaFactory = new InstaFactory();
                        var instaApi = await instaFactory.BuildInstaApi(instaUser.Login, instaUser.Password);

                        var executor = GetBaseExecutor(queueItem, instaApi, db);
                        await executor.Run(queueItem);
                    }
                }
            }

            Console.WriteLine("Instagram service is stopped.");
        }

        private IBaseExecutor GetBaseExecutor(QueueItem queueItem, IInstaApi instaApi, ApiContext db)
        {
            IBaseExecutor executor;
            switch (queueItem.QueueType)
            {
                case QueueType.Like:
                    executor = new LikeExecutor(instaApi, db);
                    break;
                default:
                    executor = new LikeExecutor(instaApi, db);
                    break;
            }
            
            return executor;
        }
    }
}
