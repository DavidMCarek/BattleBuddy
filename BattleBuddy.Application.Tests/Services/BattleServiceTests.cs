using BattleBuddy.Application.Interfaces.Repositories;
using BattleBuddy.Application.Services;
using BattleBuddy.Domain.Entities;
using Moq;

namespace BattleBuddy.Application.Tests;

[TestFixture]
public class BattleServiceTests
{
    private readonly Mock<ICharacterRepository> _characterRepoMock;
    private readonly Mock<IStatusRepository> _statusRepoMock;
    private readonly BattleService _service;

    public BattleServiceTests()
    {
        _characterRepoMock = new Mock<ICharacterRepository>();
        _statusRepoMock = new Mock<IStatusRepository>();
        _service = new BattleService(_characterRepoMock.Object, _statusRepoMock.Object);
    }

    [Test]
    public async Task CreateCharacterAsyncShouldReturnCharacterWithCorrectValues()
    {
        // Arrange
        string name = "Gopher";
        int hp = 20;

        Character? created = null;

        _characterRepoMock
            .Setup(repo => repo.AddCharacterAsync(It.IsAny<Character>()))
            .Callback<Character>(c => created = c)
            .Returns(Task.CompletedTask);

        _characterRepoMock
            .Setup(repo => repo.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var result = await _service.CreateCharacterAsync(name, hp);

        Assert.That(result.Name, Is.EqualTo(name));
        Assert.That(result.CurrentHitPoints, Is.EqualTo(hp));
        Assert.That(result.TotalHitPoints, Is.EqualTo(hp));
        Assert.That(result.IsDown, Is.False);
    }

    [Test]
    public void ApplyStatusEffectAsyncShouldThrowIfStatusNotFound()
    {
        _statusRepoMock.Setup(repo => repo.GetStatusByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Status)null!);

        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.ApplyStatusEffectAsync(1, 1));
    }

    [Test]
    public void ApplyStatusEffectAsyncShouldThrowIfCharacterNotFound()
    {
        _statusRepoMock.Setup(repo => repo.GetStatusByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Status { Name = "Stunned", Description = "Skip an action." });

        _characterRepoMock.Setup(repo => repo.GetCharacterByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Character)null!);

        Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.ApplyStatusEffectAsync(1, 1));
    }

    [Test]
    public async Task ApplyStatusEffectAsyncShouldAddStatusAndSaves()
    {
        var character = new Character { Name = "Gopher" };
        var status = new Status { Name = "Stunned", Description = "Lose an action." };

        _statusRepoMock.Setup(r => r.GetStatusByIdAsync(1)).ReturnsAsync(status);
        _characterRepoMock.Setup(r => r.GetCharacterByIdAsync(1)).ReturnsAsync(character);
        _characterRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _service.ApplyStatusEffectAsync(1, 1);

        // Assert
        Assert.That(result, Is.EqualTo(character));
        _characterRepoMock.Verify(r => r.UpdateCharacter(character), Times.Once);
        _characterRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public void AttackAsyncShouldThrowIfCharacterNotFound()
    {
        _characterRepoMock.Setup(r => r.GetCharacterByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Character)null!);

        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.AttackAsync(5, 99));
    }

    [Test]
    public async Task AttackAsyncShouldReduceHealthAndSaves()
    {
        var character = new Character { Name = "Fizban", CurrentHitPoints = 10, TotalHitPoints = 10 };
        _characterRepoMock.Setup(r => r.GetCharacterByIdAsync(1)).ReturnsAsync(character);
        _characterRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await _service.AttackAsync(5, 1);

        Assert.That(result.CurrentHitPoints, Is.EqualTo(5));
        _characterRepoMock.Verify(r => r.UpdateCharacter(character), Times.Once);
    }

    [Test]
    public void HealAsyncShouldThrowIfCharacterNotFound()
    {
        _characterRepoMock.Setup(r => r.GetCharacterByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Character)null!);

        Assert.ThrowsAsync<KeyNotFoundException>(() => _service.HealAsync(5, 99));
    }

    [Test]
    public async Task HealAsyncShouldIncreaseHealthAndSaves()
    {
        var character = new Character { Name = "Fizban", CurrentHitPoints = 5, TotalHitPoints = 10 };
        _characterRepoMock.Setup(r => r.GetCharacterByIdAsync(1)).ReturnsAsync(character);
        _characterRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await _service.HealAsync(3, 1);

        Assert.That(result.CurrentHitPoints, Is.EqualTo(8));
        _characterRepoMock.Verify(r => r.UpdateCharacter(character), Times.Once);
    }
}
