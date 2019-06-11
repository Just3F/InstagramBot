namespace InstagramBot.DB.Enums
{
    public enum LoginStatus
    {
        UnAuthenticated = 0,
        Authenticated = 1,
        WaitForCodeChallengeRequired = 3,
        WaitForPassChallengeRequired = 4,
    }
}
