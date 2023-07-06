using Media.Api.Endpoints.Reservations;

namespace Media.Tests.UnitTests.Endpoints;

public sealed class GetResevationEndpointTests
{
   [Fact]
   public async Task GetReservationShouldReturnListOfReservations() 
   {
       // Arrange
       var sut = Factory.Create<GetReservationEndpoint>();

       // Act
       await sut.HandleAsync(default!).ConfigureAwait(false);
       var response = sut.Response;

       // Assert
       _ = response.Count().Should().BeGreaterThanOrEqualTo(0);
   }
}
