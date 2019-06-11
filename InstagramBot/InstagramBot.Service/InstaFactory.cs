using System;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using InstagramBot.DB.Entities;
using InstagramBot.DB.Enums;
using InstagramBot.Service.Database;
using Microsoft.EntityFrameworkCore;

namespace InstagramBot.Service
{
    public class InstaFactory
    {
        IInstaApi _instaApi;

        public async Task<string> SetSession(InstagramUser user)
        {
            _instaApi = BuildApi(user.Login, user.Password);
            try
            {
                _instaApi.LoadStateDataFromString(user.Session);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (user.LoginStatus == LoginStatus.WaitForCheckChallengeRequiredCode)
            {
                var verifyResult = await _instaApi.VerifyCodeForChallengeRequireAsync(user.ChallengeRequiredCode);
                if (verifyResult.Succeeded)
                {
                    user.LoginStatus = LoginStatus.Authenticated;
                    user.Session = _instaApi.GetStateDataAsString();
                    return user.Session;
                }
            }

            if (!_instaApi.IsUserAuthenticated)
            {
                var logInResult = await _instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                {
                    if (logInResult.Value == InstaLoginResult.ChallengeRequired)
                    {
                        var challenge = await _instaApi.GetChallengeRequireVerifyMethodAsync();
                        if (challenge.Succeeded)
                        {
                            //challenge.Value.
                            var email = await _instaApi.RequestVerifyCodeToSMSForChallengeRequireAsync(replayChallenge: true);
                            if (email.Succeeded)
                            {
                                user.LoginStatus = LoginStatus.WaitForCheckChallengeRequiredCode;
                                user.ChallengeRequiredCode = string.Empty;
                                return string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    user.LoginStatus = LoginStatus.Authenticated;
                    user.Session = _instaApi.GetStateDataAsString();
                    return user.Session;
                }
            }

            return string.Empty;
        }

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
                    if (!logInResult.Succeeded && logInResult.Value == InstaLoginResult.ChallengeRequired)
                    {
                        var challenge = await _instaApi.GetChallengeRequireVerifyMethodAsync();
                        if (challenge.Succeeded)
                        {
                            //challenge.Value.
                            var email = await _instaApi.RequestVerifyCodeToEmailForChallengeRequireAsync(replayChallenge: true);
                            if (email.Succeeded)
                            {
                                instagramUser.LoginStatus = LoginStatus.WaitForChallengeRequiredCode;
                                //var verifyLogin = await _instaApi.VerifyCodeForChallengeRequireAsync("CODE");
                            }
                        }


                        Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
                        return null;
                    }

                    instagramUser.Session = _instaApi.GetStateDataAsString();
                    await db.SaveChangesAsync();
                }
            }

            return _instaApi;
        }

        void SaveSession()
        {
            if (_instaApi == null)
                return;
            if (!_instaApi.IsUserAuthenticated)
                return;
            _instaApi.SessionHandler.Save();
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
