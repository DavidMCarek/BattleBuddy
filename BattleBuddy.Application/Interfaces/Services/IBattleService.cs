using BattleBuddy.Domain.Entities;

namespace BattleBuddy.Application.Interfaces.Services;

public interface IBattleService
{
    Task<Character> AttackAsync(int damage, int characterId);
    Task<Character> HealAsync(int health, int characterId);
    Task<Character> ApplyStatusEffectAsync(int statusId, int characterId);
    Task<Character> RemoveStatusEffectAsync(int statusId, int characterId);
    Task<Character> CreateCharacterAsync(string name, int totalHitPoints);
    Task<Character?> GetCharacterAsync(int id);
    Task<IEnumerable<Character>> GetAllCharactersAsync();
    Task<Status> CreateStatusAsync(string name, string description);
    Task<Status?> GetStatusAsync(int statusId);
    Task<IEnumerable<Status>> GetAllStatusesAsync();
}