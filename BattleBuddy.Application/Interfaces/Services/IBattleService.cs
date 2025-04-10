using BattleBuddy.Domain.Entities;

namespace BattleBuddy.Application.Interfaces.Services;

public interface IBattleService
{
    Task<Character> AttackAsync(int damage, int characterId);
    Task<Character> HealAsync(int health, int characterId);
    Task<Character> ApplyStatusEffectAsync(int statusId, int characterId);
    Task<Character> CreateCharacterAsync(string name, int totalHitPoints);
    Task<IEnumerable<Character>> GetAllCharactersAsync();
    Task<Status> CreateStatusAsync(string name, string description);
    Task<IEnumerable<Status>> GetAllStatusesAsync();
}