using Microsoft.EntityFrameworkCore;
using mvc_query_hub.Models;

namespace mvc_query_hub.Data
{
    public class HubContext : DbContext

    {
        public DbSet<PipingQuery> Queries { get; set; } = default!;
        public DbSet<QueryComments> Comments { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;

        public HubContext(DbContextOptions<HubContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=QueryHub.db");
        }
    }
}
