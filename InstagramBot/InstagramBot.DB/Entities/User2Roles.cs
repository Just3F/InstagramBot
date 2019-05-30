using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstagramBot.DB.Entities
{
    public class User2Roles
    {
        [Key]
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        [Key]
        public long RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }

    public class User2RolesEntityConfiguration : IEntityTypeConfiguration<User2Roles>
    {
        public void Configure(EntityTypeBuilder<User2Roles> builder)
        {
            builder.HasKey(x => new { x.RoleId, x.UserId });

            builder.HasOne(x => x.Role)
                .WithMany(x => x.User2Roles)
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.User2Roles)
                .HasForeignKey(x => x.UserId);
        }
    }
}
