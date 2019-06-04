using System;
using System.Linq;
using System.Threading.Tasks;
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
                    var queueItem = await db.QueueItems.Include(x=>x.InstagramUser).FirstOrDefaultAsync();
                    var instaUser = queueItem.InstagramUser;

                    InstaFactory instaFactory = new InstaFactory();
                    var instaApi = await instaFactory.BuildInstaApi(instaUser.Login, instaUser.Password);
                    IBaseExecutor executor = new LikeExecutor(instaApi, db);
                    await executor.Execute(queueItem);
                }
            }

            Console.WriteLine("Instagram service is stopped.");
        }
    }
}
