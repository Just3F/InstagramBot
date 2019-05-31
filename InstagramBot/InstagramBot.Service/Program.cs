using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Enums;
using InstagramBot.Service.Database;
using Microsoft.EntityFrameworkCore;

namespace InstagramBot.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new ApiContextFactory().CreateDbContext())
            //{
            //    db.Database.Migrate();

            //    var instagramUser = db.InstagramUsers.FirstOrDefaultAsync().Result;

            //    var instaFactory = new InstaFactory();
            //    var s = instaFactory.BuildInstaApi(instagramUser.Login, instagramUser.Password).Result;
            //}

            var instaService = new InstaService().Build();
            instaService.Run();

            Console.ReadLine();
        }
    }
}
