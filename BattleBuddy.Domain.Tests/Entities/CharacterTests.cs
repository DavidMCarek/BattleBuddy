using BattleBuddy.Domain.Entities;

[TestFixture]
public class CharacterTests
{
    [Test]
    public void AddStatusShouldAddNewStatus()
    {
        var character = new Character { Name = "Gopher" };
        var status = new Status { Id = 1, Name = "Poisoned", Description = "Take 1 point of damage each turn." };

        character.AddStatus(status);

        Assert.That(character.Statuses, Has.Exactly(1).EqualTo(status));
    }

    [Test]
    public void AddStatusShouldIgnoresDuplicateStatusById()
    {
        var character = new Character { Name = "Gopher" };
        var status = new Status { Id = 1, Name = "Poisoned", Description = "Take 1 point of damage each turn." };

        character.AddStatus(status);
        character.AddStatus(status); // Should be ignored

        Assert.That(character.Statuses.Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveStatusShouldRemoveExistingStatus()
    {
        var character = new Character { Name = "Gopher" };
        var status = new Status { Id = 1, Name = "Stunned", Description = "Lose an action" };

        character.AddStatus(status);
        character.RemoveStatus(status.Id);

        Assert.That(character.Statuses, Is.Empty);
    }

    [Test]
    public void RemoveStatusShouldIgnoresMissingStatus()
    {
        var character = new Character { Name = "Gopher" };

        // Should not throw or affect anything
        character.RemoveStatus(999);

        Assert.That(character.Statuses, Is.Empty);
    }

    [TestCase(-20, 80, false)]
    [TestCase(-100, 0, true)]
    [TestCase(10, 110, false)]
    public void ModifyHealthShouldUpdateHitPointsAndIsDownState(int change, int expectedHp, bool expectedDown)
    {
        var character = new Character { Name = "Gopher", HitPoints = 100 };

        character.ModifyHealth(change);

        Assert.Multiple(() =>
        {
            Assert.That(character.HitPoints, Is.EqualTo(expectedHp));
            Assert.That(character.IsDown, Is.EqualTo(expectedDown));
        });

    }
}