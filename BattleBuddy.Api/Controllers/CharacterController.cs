using BattleBuddy.Application.Interfaces.Services;
using BattleBuddy.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BattleBuddy.Api.Controllers;

[ApiController]
[Route("Characters")]
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

    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequest request)
    {
        var character = await _battleService.CreateCharacterAsync(request.Name, request.HitPoints);
        return CreatedAtAction($"/characters/{character.Id}", character);
    }
}

public class CreateCharacterRequest
{
    public string Name { get; set; }
    public int HitPoints { get; set; }
}
