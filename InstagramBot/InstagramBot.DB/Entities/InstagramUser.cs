using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using InstagramBot.DB.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstagramBot.DB.Entities
{
    public class InstagramUser : AuditEntity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Session { get; set; }
        public LoginStatus LoginStatus { get; set; }

        public long AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public List<QueueItem> QueueItems { get; set; } = new List<QueueItem>();
    }

    public class InstagramUserEntityConfiguration : IEntityTypeConfiguration<InstagramUser>
    {
        public void Configure(EntityTypeBuilder<InstagramUser> builder)
        {
        }
    }
}
