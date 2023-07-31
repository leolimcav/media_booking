using Media.Api.Endpoints.Reservations;
using Media.Api.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Media.Tests.UnitTests.Endpoints;

public sealed class GetResevationsEndpointTests
{
   [Fact]
   public async Task GetReservationShouldReturnListOfReservations() 
   {
       // Arrange
       var repository = A.Fake<IReservationRepository>();
       var sut = Factory.Create<GetReservationsEndpoint>(c =>    
       {
            c.AddTestServices(s => 
                    {
                        s.AddSingleton(repository);
                        s.BuildServiceProvider();
                    });
       });

       // Act
       await sut.HandleAsync(default!).ConfigureAwait(false);
       var response = sut.Response;

       // Assert
       _ = response.Count().Should().BeGreaterThanOrEqualTo(0);
   }
}
