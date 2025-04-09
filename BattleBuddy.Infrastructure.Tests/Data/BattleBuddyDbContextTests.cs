using BattleBuddy.Domain.Entities;
using BattleBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BattleBuddy.Infrastructure.Tests;

[TestFixture]
public class BattleBuddyDbContextTests
{
    private DbContextOptions<BattleBuddyDbContext> _options;

    [SetUp]
    public void SetUp()
    {
        _options = new DbContextOptionsBuilder<BattleBuddyDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
            .Options;
    }

    [Test]
    public void CanAddCharacterWithStatuses()
    {
        // Arrange
        var character = new Character
        {
            Id = 1,
            Name = "Test Character",
            Statuses = new List<Status>
            {
                new Status { Id = 1, Name = "Stunned", Description = "Lose an action" },
                new Status { Id = 2, Name = "Posioned", Description = "Lose 1 HP each turn" }
            }
        };

        // Act
        using (var context = new BattleBuddyDbContext(_options))
        {
            context.Characters.Add(character);
            context.SaveChanges();
        }

        // Assert
        using (var context = new BattleBuddyDbContext(_options))
        {
            var firstCharacter = context.Characters
                .Include(c => c.Statuses)
                .FirstOrDefault();

            Assert.That(firstCharacter, Is.Not.Null);
            Assert.That(firstCharacter.Statuses, Has.Count.EqualTo(2));
        }
    }

    [Test]
    public void CanUpdateCharacter()
    {

    }

    [Test]
    public void CanDeleteCharacter()
    {

    }

    [Test]
    public void CanAddStatus()
    {

    }

    [Test]
    public void CanUpdateStatus()
    {

    }

    [Test]
    public void CanDeleteStatus()
    {

    }
}

