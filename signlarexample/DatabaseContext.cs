using Microsoft.EntityFrameworkCore;
using signlarexample.Models;

namespace signlarexample
{
    public class DatabaseContext : DbContext
    {
        public DbSet<PushSubscription> PushSubscription { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=webnotifications.db");
        }
    }
}