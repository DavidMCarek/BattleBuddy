namespace BattleBuddy.Api.RequestModels;

public class CreateCharacterRequest
{
    public required string Name { get; set; }
    public int HitPoints { get; set; }
}