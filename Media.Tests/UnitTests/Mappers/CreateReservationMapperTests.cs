using Media.Api.Endpoints.Reservations;
using Media.Api.Entities;
using Media.Api.Mappers;

namespace Media.Tests.UnitTests.Mappers;

public sealed class CreateReservationMapperTests
{
    [Fact]
    public void FromEntityShouldThrowArgumentNullExceptionWhenEntityIsNull()
    {
        // Arrange
        var mapper = new CreateReservationMapper();

        // Act
        var action = () => _ = mapper.FromEntity(default!);

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should().Be("e");
    }

    [Fact]
    public void ToEntityShouldThrowArgumentNullExceptionWhenDtoIsNull()
    {
        // Arrange
        var mapper = new CreateReservationMapper();

        // Act
        var action = () => _ = mapper.ToEntity(default!);

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>()
            .Which.ParamName.Should().Be("r");
    }

    [Fact]
    public void FromEntityShouldReturnADtoWhenEntityIsValid()
    {
        // Arrange
        var mapper = new CreateReservationMapper();
        var reservation = new Reservation 
        {
            Id = 1,
            Name = "teste",
            Device = "teste",
            Classroom = "teste",
            Date = DateTime.Now,
        };

        // Act
        var dto = mapper.FromEntity(reservation);

        // Assert
        _ = dto.Should().BeOfType<CreateReservationResponseDto>();
        _ = dto.Id.Should().Be(reservation.Id);
    }

    [Fact]
    public void ToEntityShouldReturnAnEntityWhenDtoIsValid()
    {
        // Arrange
        var mapper = new CreateReservationMapper();
        var dto = new CreateReservationRequestDto("teste", "teste", "teste", DateTime.Now);

        // Act
        var entity = mapper.ToEntity(dto);

        // Assert
        _ = entity.Should().BeOfType<Reservation>();
        _ = entity.Name.Should().Be(dto.Name);
    }
}
