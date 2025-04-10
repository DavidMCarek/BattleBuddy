using BattleBuddy.Application.Interfaces.Repositories;
using BattleBuddy.Application.Interfaces.Services;
using BattleBuddy.Domain.Entities;

namespace BattleBuddy.Application.Services;

public class BattleService : IBattleService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IStatusRepository _statusRepository;
    public BattleService(ICharacterRepository characterRepository, IStatusRepository statusRepository)
    {
        _characterRepository = characterRepository;
        _statusRepository = statusRepository;
    }

    public async Task<Character> ApplyStatusEffectAsync(int statusId, int characterId)
    {
        var status = await _statusRepository.GetStatusByIdAsync(statusId);
        if (status == null)
            throw new KeyNotFoundException($"Status with id '{statusId}' not found");

        var character = await _characterRepository.GetCharacterByIdAsync(characterId);
        if (character == null)
            throw new KeyNotFoundException($"Character with id '{characterId}' not found");

        character.AddStatus(status);
        _characterRepository.UpdateCharacter(character);

        await _characterRepository.SaveChangesAsync();

        return character;
    }

    public async Task<Character> AttackAsync(int damage, int characterId)
    {
        var character = await _characterRepository.GetCharacterByIdAsync(characterId);

        if (character == null)
            throw new KeyNotFoundException($"Character with id '{characterId}' not found");

        character.ModifyHealth(-damage);

        _characterRepository.UpdateCharacter(character);

        await _characterRepository.SaveChangesAsync();

        return character;
    }

    public async Task<Character> CreateCharacterAsync(string name, int totalHitPoints)
    {
        var character = new Character
        {
            Name = name,
            CurrentHitPoints = totalHitPoints,
            TotalHitPoints = totalHitPoints,
            IsDown = false
        };

        await _characterRepository.AddCharacterAsync(character);
        await _characterRepository.SaveChangesAsync();

        return character;
    }

    public async Task<Status> CreateStatusAsync(string name, string description)
    {
        var status = new Status
        {
            Name = name,
            Description = description
        };

        await _statusRepository.AddStatusAsync(status);
        await _statusRepository.SaveChangesAsync();

        return status;
    }

    public Task<IEnumerable<Character>> GetAllCharactersAsync()
    {
        return _characterRepository.GetAllCharactersAsync();
    }

    public Task<IEnumerable<Status>> GetAllStatusesAsync()
    {
        return _statusRepository.GetAllStatusesAsync();
    }

    public Task<Character?> GetCharacterAsync(int id)
    {
        return _characterRepository.GetCharacterByIdAsync(id);
    }

    public Task<Status?> GetStatusAsync(int statusId)
    {
        return _statusRepository.GetStatusByIdAsync(statusId);
    }

    public async Task<Character> HealAsync(int health, int characterId)
    {
        var character = await _characterRepository.GetCharacterByIdAsync(characterId);

        if (character == null)
            throw new KeyNotFoundException($"Character with id '{characterId}' not found");

        character.ModifyHealth(health);

        _characterRepository.UpdateCharacter(character);

        await _characterRepository.SaveChangesAsync();

        return character;
    }

    public async Task<Character> RemoveStatusEffectAsync(int statusId, int characterId)
    {
        var character = await _characterRepository.GetCharacterByIdAsync(characterId);

        if (character == null)
            throw new KeyNotFoundException($"Character with id '{characterId}' not found");

        var status = character.Statuses.FirstOrDefault(s => s.Id == statusId);

        // No need to remove a status that's not present on a character
        if (status == null)
            return character;

        character.Statuses.Remove(status);

        _characterRepository.UpdateCharacter(character);

        await _characterRepository.SaveChangesAsync();

        return character;
    }
}