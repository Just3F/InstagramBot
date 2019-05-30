using System;

namespace InstagramBot.DB.Entities
{
    public class AuditEntity : BaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
