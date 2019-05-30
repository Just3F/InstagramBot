using System;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using InstagramBot.Service.Database;
using Microsoft.EntityFrameworkCore;

namespace InstagramBot.Service
{
    public class InstaFactory
    {
        IInstaApi _instaApi;

        public async Task<IInstaApi> BuildInstaApi(string login, string password)
        {
            using (var db = new ApiContextFactory().CreateDbContext())
            {
                var instagramUser = await db.InstagramUsers.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

                _instaApi = BuildApi(login, password);

                try
                {
                    _instaApi.LoadStateDataFromString(instagramUser.Session);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (!_instaApi.IsUserAuthenticated)
                {
                    // login
                    Console.WriteLine($"Logging in as {login}");
                    var logInResult = await _instaApi.LoginAsync();
                    if (!logInResult.Succeeded)
                    {
                        Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
                        return null;
                    }

                    instagramUser.Session = _instaApi.GetStateDataAsString();
                    await db.SaveChangesAsync();
                }
            }

            return _instaApi;
        }


        IInstaApi BuildApi(string login, string password)
        {
            return InstaApiBuilder.CreateBuilder()
                .SetUser(UserSessionData.ForUsername(login).WithPassword(password))
                .UseLogger(new DebugLogger(LogLevel.All))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                // Session handler, set a file path to save/load your state/session data
                //.SetSessionHandler(new FileSessionHandler() { FilePath = StateFile })
                .Build();
        }
    }
}
