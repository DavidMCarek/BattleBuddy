using BattleBuddy.Api.RequestModels;
using BattleBuddy.Application.Interfaces.Services;
using BattleBuddy.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BattleBuddy.Api.Controllers;

[Route("api/statuses")]
public class StatusController : ControllerBase
{
    private readonly IBattleService _battleService;

    public StatusController(IBattleService battleService)
    {
        _battleService = battleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetAllStatuses()
    {
        var statuses = await _battleService.GetAllStatusesAsync();
        return Ok(statuses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Character>> GetStatus(int id)
    {
        var status = await _battleService.GetStatusAsync(id);

        if (status == null)
            return NotFound();

        return Ok(status);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStatus([FromBody] CreateStatusRequest request)
    {
        var character = await _battleService.CreateStatusAsync(request.Name, request.Description);

        return CreatedAtAction(
            nameof(GetStatus),
            new { id = character.Id },
            character
        );
    }
}
