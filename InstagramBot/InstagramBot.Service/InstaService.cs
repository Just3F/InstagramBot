using System;
using System.Threading.Tasks;
using InstagramBot.Service.Database;
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
                    var queueItems = await db.QueueItems.Include(x=>x.InstagramUser).ToListAsync();
                }
            }

            Console.WriteLine("Instagram service is stopped.");
        }
    }
}
