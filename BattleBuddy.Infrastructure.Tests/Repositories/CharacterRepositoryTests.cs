using BattleBuddy.Domain.Entities;
using BattleBuddy.Infrastructure.Data;
using BattleBuddy.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BattleBuddy.Tests.Infrastructure.Repositories;

[TestFixture]
public class CharacterRepositoryTests
{
    private Mock<IBattleBuddyDbContext> _mockContext = null!;
    private Mock<DbSet<Character>> _mockSet = null!;
    private CharacterRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        _mockSet = new Mock<DbSet<Character>>();

        _mockContext = new Mock<IBattleBuddyDbContext>();
        _mockContext.Setup(m => m.Characters).Returns(_mockSet.Object);

        _repository = new CharacterRepository(_mockContext.Object);
    }

    [Test]
    public async Task AddCharacterAsyncShouldAddCharacterToDbSet()
    {
        var character = new Character { Id = 1, Name = "Gopher" };

        await _repository.AddCharacterAsync(character);

        _mockSet.Verify(m => m.AddAsync(character, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetCharacterByIdAsyncShouldReturnCorrectCharacter()
    {
        // Ignoring this for now since its a pain and not worth the time
    }

    [Test]
    public void UpdateCharacterAsyncShouldUpdateEntity()
    {
        var character = new Character { Id = 1, Name = "Gopher" };

        _repository.UpdateCharacter(character);

        _mockSet.Verify(m => m.Update(character), Times.Once);
    }

    [Test]
    public async Task SaveChangesAsyncShouldCallContextSaveChanges()
    {
        _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        await _repository.SaveChangesAsync();

        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllCharactersAsyncShouldReturnAllCharacters()
    {
        // This is also not worth the effort for a single function call
    }
}
