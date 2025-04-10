using BattleBuddy.Api.RequestModels;
using BattleBuddy.Application.Interfaces.Services;
using BattleBuddy.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BattleBuddy.Api.Controllers;

[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    private readonly IBattleService _battleService;

    public CharacterController(IBattleService battleService)
    {
        _battleService = battleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetAllCharacters()
    {
        var characters = await _battleService.GetAllCharactersAsync();
        return Ok(characters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Character>> GetCharacter(int id)
    {
        var character = await _battleService.GetCharacterAsync(id);

        if (character == null)
            return NotFound();

        return Ok(character);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequest request)
    {
        var character = await _battleService.CreateCharacterAsync(request.Name, request.HitPoints);

        return CreatedAtAction(
            nameof(GetCharacter),
            new { id = character.Id },
            character
        );
    }

    [HttpPut("{characterId}/attack/{damage}")]
    public async Task<ActionResult<Character>> AttackCharacter(int characterId, int damage)
    {
        var character = await _battleService.AttackAsync(damage, characterId);

        return Ok(character);
    }

    [HttpPut("{characterId}/heal/{health}")]
    public async Task<ActionResult<Character>> HealCharacter(int characterId, int health)
    {
        var character = await _battleService.HealAsync(health, characterId);

        return Ok(character);
    }

    [HttpPut("{characterId}/status/apply/{statusId}")]
    public async Task<ActionResult> ApplyStatusEffect(int characterId, int statusId)
    {
        var character = await _battleService.ApplyStatusEffectAsync(statusId, characterId);

        return Ok(character);
    }

    [HttpPut("{characterId}/status/remove/{statusId}")]
    public async Task<ActionResult> RemoveStatusEffect(int characterId, int statusId)
    {
        var character = await _battleService.RemoveStatusEffectAsync(statusId, characterId);

        return Ok(character);
    }
}
