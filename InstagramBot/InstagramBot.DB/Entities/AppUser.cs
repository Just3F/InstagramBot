using System.Collections.Generic;

namespace InstagramBot.DB.Entities
{
    public class AppUser : AuditEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public List<User2Roles> User2Roles { get; set; } = new List<User2Roles>();
    }
}
