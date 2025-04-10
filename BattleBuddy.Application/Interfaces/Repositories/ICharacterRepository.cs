using BattleBuddy.Domain.Entities;

namespace BattleBuddy.Application.Interfaces.Repositories;

public interface ICharacterRepository
{
    Task AddCharacterAsync(Character character);
    Task<Character?> GetCharacterByIdAsync(int id);
    void UpdateCharacterAsync(Character character);
    Task SaveChangesAsync();
}