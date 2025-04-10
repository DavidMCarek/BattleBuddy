using BattleBuddy.Application.Interfaces.Repositories;
using BattleBuddy.Domain.Entities;
using BattleBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BattleBuddy.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{

    private readonly IBattleBuddyDbContext _context;

    public CharacterRepository(IBattleBuddyDbContext context)
    {
        _context = context;
    }

    public async Task AddCharacterAsync(Character character)
    {
        await _context.Characters.AddAsync(character);
    }

    public async Task<Character?> GetCharacterByIdAsync(int id)
    {
        return await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void UpdateCharacter(Character character)
    {
        _context.Characters.Update(character);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    // In a real world scenario this would need paginated
    public async Task<IEnumerable<Character>> GetAllCharactersAsync()
    {
        return await _context.Characters.ToListAsync();
    }
}