using System;
using System.Linq;
using InstagramBot.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InstagramBot.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApiContextFactory().CreateDbContext())
            {

                Console.WriteLine(context.Users.FirstOrDefault().Login);

            }
            Console.ReadLine();
        }
    }

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
