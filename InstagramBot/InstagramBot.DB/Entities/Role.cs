using System.Collections.Generic;

namespace InstagramBot.DB.Entities
{
    public class Role : AuditEntity
    {
        public string Name { get; set; }

        public List<User2Roles> User2Roles { get; set; } = new List<User2Roles>();
    }
}
