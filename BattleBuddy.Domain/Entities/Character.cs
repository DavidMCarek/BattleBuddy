namespace BattleBuddy.Domain.Entities;

public class Character
{
    // Normally I'd use a guid for something like this but for simplicity
    // I'll stick with an int
    public int Id { get; set; }
    public required string Name { get; set; }
    public int HitPoints { get; private set; }
    public bool IsDown { get; set; }
    public bool IsNpc { get; set; }
    public int SavesRemaining { get; set; } = 3;


    private readonly List<Status> _statuses = [];
    public IReadOnlyCollection<Status> Statuses => _statuses.AsReadOnly();

    public void AddStatus(Status status)
    {
        if (_statuses.Any(s => s.Id == status.Id))
            throw new InvalidOperationException($"Character already has a status of type {status.Name}.");

        _statuses.Add(status);
    }

    public void RemoveStatus(int statusId)
    {
        var statusToRemove = _statuses.FirstOrDefault(s => s.Id == statusId);
        // If the status is not on the character we can just do nothing here
        if (statusToRemove == null)
            return;

        _statuses.Remove(statusToRemove);
    }

    public void TakeDamage(int hpDamage)
    {
        if (hpDamage < 0)
            throw new ArgumentException("Character cannot take negative damage");

        HitPoints -= hpDamage;
    }

    public void Heal(int hp)
    {
        if (hp < 0)
            throw new ArgumentException("Character cannot heal negative hp");

        HitPoints += hp;
    }
}