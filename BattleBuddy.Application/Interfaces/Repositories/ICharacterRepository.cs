using BattleBuddy.Domain.Entities;

namespace BattleBuddy.Application.Interfaces.Repositories;

public interface ICharacterRepository
{
    Task AddCharacterAsync(Character character);
    Task<Character?> GetCharacterByIdAsync(int id);
    void UpdateCharacter(Character character);
    Task SaveChangesAsync();
    Task<IEnumerable<Character>> GetAllCharactersAsync();
}