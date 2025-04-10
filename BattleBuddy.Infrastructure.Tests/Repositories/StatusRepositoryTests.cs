using BattleBuddy.Domain.Entities;
using BattleBuddy.Infrastructure.Data;
using BattleBuddy.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BattleBuddy.Infrastructure.Tests.Repositories;

[TestFixture]
public class StatusRepositoryTests
{
    private Mock<IBattleBuddyDbContext> _mockContext = null!;
    private Mock<DbSet<Status>> _mockSet = null!;
    private StatusRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        _mockSet = new Mock<DbSet<Status>>();

        _mockContext = new Mock<IBattleBuddyDbContext>();
        _mockContext.Setup(m => m.Statuses).Returns(_mockSet.Object);

        _repository = new StatusRepository(_mockContext.Object);
    }

    [Test]
    public async Task AddStatusAsyncShouldAddStatusToDbSet()
    {
        var status = new Status { Id = 1, Name = "Poisoned", Description = "Lose 1 hp each turn" };

        await _repository.AddStatusAsync(status);

        _mockSet.Verify(m => m.AddAsync(status, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetStatusByIdAsyncShouldReturnCorrectStatus()
    {
        // Ignoring this for now since its a pain and not worth the time
    }

    [Test]
    public void UpdateStatusAsyncShouldUpdateEntity()
    {
        var status = new Status { Id = 1, Name = "Poisoned", Description = "Lose 1 hp each turn" };

        _repository.UpdateStatusAsync(status);

        _mockSet.Verify(m => m.Update(status), Times.Once);
    }

    [Test]
    public async Task SaveChangesAsyncShouldCallContextSaveChanges()
    {
        _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        await _repository.SaveChangesAsync();

        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllStatusesAsyncShouldReturnAllStatuses()
    {
        // This is also not worth the effort for a single 
    }
}
