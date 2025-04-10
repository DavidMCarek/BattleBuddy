using BattleBuddy.Domain.Entities;

namespace BattleBuddy.Application.Interfaces.Repositories;

public interface IStatusRepository
{
    Task AddStatusAsync(Status status);
    Task<Status?> GetStatusByIdAsync(int id);
    void UpdateStatusAsync(Status status);
    Task SaveChangesAsync();
    Task<IEnumerable<Status>> GetAllStatusesAsync();
}