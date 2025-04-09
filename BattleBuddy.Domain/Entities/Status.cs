namespace BattleBuddy.Domain.Entities;

public class Status
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}