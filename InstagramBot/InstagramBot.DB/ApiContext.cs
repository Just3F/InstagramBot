using System;
using System.Linq;
using System.Threading.Tasks;
using InstagramBot.DB.Entities;
using InstagramBot.DB.Enums;
using Microsoft.EntityFrameworkCore;

namespace InstagramBot.DB
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InstagramUser> InstagramUsers { get; set; }
        public DbSet<QueueItem> QueueItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema: DbGlobals.SchemaName);

            modelBuilder.Entity<AppUser>().HasData(new AppUser { Id = 1, Login = "Admin", Password = "123456", });
            modelBuilder.Entity<InstagramUser>().HasData(new InstagramUser { Id = 1, Login = "belarus.here", Password = "Gfhjkm63934710", AppUserId = 1});
            modelBuilder.Entity<QueueItem>().HasData(new QueueItem { Id = 1, InstagramUserId = 1, QueueType = QueueType.InProgress});

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuditInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is AuditEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AuditEntity)entry.Entity).Created = DateTime.UtcNow;
                }
                ((AuditEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}
