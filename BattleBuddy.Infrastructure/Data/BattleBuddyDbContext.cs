using BattleBuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleBuddy.Infrastructure.Data;

public class BattleBuddyDbContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<Status> Statuses { get; set; }

    public BattleBuddyDbContext(DbContextOptions<BattleBuddyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(props => props.Id);
            entity.HasMany(props => props.Statuses);
            entity.Property(p => p.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(props => props.Id);
            entity.Property(p => p.Name).HasMaxLength(100);
        });
    }
}
