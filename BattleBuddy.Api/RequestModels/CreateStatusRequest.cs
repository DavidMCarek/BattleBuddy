namespace BattleBuddy.Api.RequestModels;

public class CreateStatusRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}