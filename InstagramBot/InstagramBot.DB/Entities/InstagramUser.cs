using InstagramBot.DB.Enums;

namespace InstagramBot.DB.Entities
{
    public class InstagramUser : AuditEntity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Session { get; set; }


        public AccountStatus AccountStatus { get; set; }
    }
}
