using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BattleBuddy.Infrastructure.Data
{
    public class BattleBuddyDbContextFactory : IDesignTimeDbContextFactory<BattleBuddyDbContext>
    {
        public BattleBuddyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BattleBuddyDbContext>();
            optionsBuilder.UseSqlite("Data Source=BattleBuddy.db");

            return new BattleBuddyDbContext(optionsBuilder.Options);
        }
    }
}
