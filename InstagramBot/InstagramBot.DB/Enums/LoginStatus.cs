namespace InstagramBot.DB.Enums
{
    public enum LoginStatus
    {
        UnAuthenticated = 0,
        Authenticated = 1,
        WaitForChallengeRequiredCode = 3,
        WaitForCheckChallengeRequiredCode = 4,
    }
}
