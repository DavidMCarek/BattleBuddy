using BattleBuddy.Application.Interfaces.Repositories;
using BattleBuddy.Domain.Entities;
using BattleBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BattleBuddy.Infrastructure.Repositories;

public class StatusRepository : IStatusRepository
{
    private readonly IBattleBuddyDbContext _context;

    public StatusRepository(IBattleBuddyDbContext context)
    {
        _context = context;
    }

    public async Task AddStatusAsync(Status status)
    {
        await _context.Statuses.AddAsync(status);
    }

    public async Task<Status?> GetStatusByIdAsync(int id)
    {
        return await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
    }

    public void UpdateStatusAsync(Status status)
    {
        _context.Statuses.Update(status);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}