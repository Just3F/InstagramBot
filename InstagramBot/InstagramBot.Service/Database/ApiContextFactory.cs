using InstagramBot.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InstagramBot.Service.Database
{
    public class ApiContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        private static string _connectionString;

        public ApiContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public ApiContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<ApiContext>();
            builder.UseSqlServer(_connectionString);

            return new ApiContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            _connectionString =
                "Server=(localdb)\\mssqllocaldb; Database=InstagramDB; Trusted_Connection=True; MultipleActiveResultSets=true";
        }
    }
}
