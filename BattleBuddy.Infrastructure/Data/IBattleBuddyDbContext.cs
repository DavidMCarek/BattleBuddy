using BattleBuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BattleBuddy.Infrastructure.Data;

public interface IBattleBuddyDbContext
{
    DbSet<Character> Characters { get; }
    DbSet<Status> Statuses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}