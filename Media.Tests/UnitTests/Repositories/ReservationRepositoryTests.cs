using Media.Api.Entities;
using Media.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Media.Tests.UnitTests.Repositories;

public sealed class ReservationRepositoryTests : IDisposable
{
    private readonly IReservationRepository _repository;
    private readonly MediaDbContext _dbContext;

    public ReservationRepositoryTests()
    {
        var contextOptions = new DbContextOptionsBuilder<MediaDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        this._dbContext = new MediaDbContext(contextOptions);
        this._repository = new ReservationRepository(this._dbContext);
    }

    public void Dispose()
    {
        ((IDisposable)_dbContext).Dispose();
    }

    [Fact]
    public async Task GetReservationsShouldReturnAnListOfReservations()
    {
        // Arrange
        this.LoadData();
        // Act
        var reservations = await this._repository.GetReservations(default!).ConfigureAwait(false);

        // Assert
        _ = reservations.Any().Should().BeTrue();
    }

    [Fact]
    public async Task CreateReservationShouldReturnCreatedEntity()
    {
        // Arrange
        var reservation = new Reservation
        {
            Name = "Teste",
            Date = DateTime.Now,
            Classroom = "class",
            Device = "dev", 
        };

        // Act
        var createdReservation = await this._repository.CreateAsync(reservation, default!).ConfigureAwait(false);

        // Assert
        _ = createdReservation.Name.Should().Be(reservation.Name);
    }

    private void LoadData() {
        _ = this._dbContext.Reservations!.Add(new Reservation 
        {
            Id = 1,
            Name = "teste",
            Classroom = "teste",
            Device = "teste",
            Date = DateTime.Now,
        });
        _ = this._dbContext.SaveChanges();
    }
}
