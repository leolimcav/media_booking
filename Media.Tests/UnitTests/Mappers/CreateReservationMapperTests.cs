using Media.Api.Endpoints.Reservations;
using Media.Api.Entities;
using Media.Api.Mappers;

namespace Media.Tests.UnitTests.Mappers;

public sealed class CreateReservationMapperTests
{
    private readonly DateTime date = DateTime.UtcNow;

    [Fact]
    public async Task FromEntityShouldThrowArgumentNullExceptionWhenEntityIsNull()
    {
        // Arrange
        var mapper = new CreateReservationMapper();

        // Act
        var action = async () => _ = await mapper.FromEntityAsync(default!, default!).ConfigureAwait(false);

        // Assert
        _ = await action.Should().ThrowExactlyAsync<ArgumentNullException>().ConfigureAwait(false);
    }

    [Fact]
    public async Task ToEntityShouldThrowArgumentNullExceptionWhenDtoIsNull()
    {
        // Arrange
        var mapper = new CreateReservationMapper();

        // Act
        var action = async () => _ = await mapper.ToEntityAsync(default!, default!).ConfigureAwait(false);
        // Assert
        _ = await action.Should().ThrowExactlyAsync<ArgumentNullException>().ConfigureAwait(false);
    }

    [Fact]
    public async Task FromEntityShouldReturnADtoWhenEntityIsValid()
    {
        // Arrange
        var mapper = new CreateReservationMapper();
        var reservation = new Reservation 
        {
            Id = 1,
            Name = "teste",
            Device = "teste",
            Classroom = "teste",
            StartDate = this.date,
            EndDate = this.date.AddHours(2)
        };

        // Act
        var dto = await mapper.FromEntityAsync(reservation, default!).ConfigureAwait(false);

        // Assert
        _ = dto.Should().BeOfType<CreateReservationResponseDto>();
        _ = dto.Id.Should().Be(reservation.Id);
    }

    [Fact]
    public async Task ToEntityShouldReturnAnEntityWhenDtoIsValid()
    {
        // Arrange
        var mapper = new CreateReservationMapper();
        var dto = new CreateReservationRequestDto("teste", "teste", "teste", this.date, this.date.AddHours(2));

        // Act
        var entity = await mapper.ToEntityAsync(dto, default!).ConfigureAwait(false);

        // Assert
        _ = entity.Should().BeOfType<Reservation>();
        _ = entity.Name.Should().Be(dto.Name);
    }
}
