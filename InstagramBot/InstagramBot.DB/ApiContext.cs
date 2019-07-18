using System;
using System.Linq;
using System.Threading.Tasks;
using InstagramBot.DB.Entities;
using InstagramBot.DB.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InstagramBot.DB
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public DbSet<QueueHistory> QueueHistories { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InstagramUser> InstagramUsers { get; set; }
        public DbSet<QueueItem> QueueItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema: DbGlobals.SchemaName);

            FillData(modelBuilder);

            modelBuilder.ApplyConfiguration(new User2RolesEntityConfiguration());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        private static void FillData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>();
            modelBuilder.Entity<InstagramUser>();
            modelBuilder.Entity<QueueItem>();
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
