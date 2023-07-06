using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Media.Api.Endpoints.Reservations;
using Media.Api.Mappers;

namespace Media.Tests.UnitTests.Endpoints;

public sealed class CreateReservationEndpointTests
{
    [Fact]
    public async Task CreateReservationShouldReturnCreatedReservation()
    {
        // Arrange
        var req = new CreateReservationRequestDto("teste", "teste", "teste", DateTime.Now);
        var linkGen = A.Dummy<LinkGenerator>();

        var sut = Factory.Create<CreateReservationEndpoint>(ctx =>
                {
                    ctx.AddTestServices(s => 
                            {
                                s.AddSingleton(linkGen);
                                s.BuildServiceProvider();
                            });
                });

        sut.Map = new ReservationMapper();

        // Act
        await sut.HandleAsync(req, default!).ConfigureAwait(false);
        var response = sut.Response;

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<CreateReservationResponseDto>();
        response.Id.Should().BeGreaterThan(0);
    }
}
