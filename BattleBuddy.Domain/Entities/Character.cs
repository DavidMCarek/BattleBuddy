namespace BattleBuddy.Domain.Entities;

public class Character
{
    // Normally I'd use a guid for something like this but for simplicity
    // I'll stick with an int
    public int Id { get; set; }
    public required string Name { get; set; }
    public int TotalHitPoints { get; set; }
    public int CurrentHitPoints { get; set; }
    public bool IsDown { get; set; }
    public ICollection<Status> Statuses { get; set; } = [];

    public void AddStatus(Status status)
    {
        // Status effects don't stack so we ignore additional added statuses
        // if they already exist on the character
        if (Statuses.Any(s => s.Id == status.Id))
            return;

        Statuses.Add(status);
    }

    public void RemoveStatus(int statusId)
    {
        var statusToRemove = Statuses.FirstOrDefault(s => s.Id == statusId);
        // If the status is not on the character we can just do nothing here
        if (statusToRemove == null)
            return;

        Statuses.Remove(statusToRemove);
    }

    public void ModifyHealth(int hpChange)
    {
        CurrentHitPoints += hpChange;
        IsDown = CurrentHitPoints <= 0;

        if (CurrentHitPoints > TotalHitPoints)
            CurrentHitPoints = TotalHitPoints;
    }
}