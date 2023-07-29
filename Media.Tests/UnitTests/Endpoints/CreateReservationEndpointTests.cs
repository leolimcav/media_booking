using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Media.Api.Endpoints.Reservations;
using Media.Api.Mappers;
using Media.Api.Repositories;

namespace Media.Tests.UnitTests.Endpoints;

public sealed class CreateReservationEndpointTests
{
    [Fact]
    public async Task CreateReservationShouldReturnCreatedReservation()
    {
        // Arrange
        var req = new CreateReservationRequestDto("teste", "teste", "teste", DateTime.Now);
        var linkGen = A.Dummy<LinkGenerator>();
        var repository = A.Fake<IReservationRepository>();

        var sut = Factory.Create<CreateReservationEndpoint>(ctx =>
                {
                    ctx.AddTestServices(s => 
                            {
                                s.AddSingleton(repository);
                                s.AddSingleton(linkGen);
                                s.BuildServiceProvider();
                            });
                });

        var mapper = new CreateReservationMapper();

        sut.Map = mapper;

        var entity = mapper.ToEntity(req);
        
        A.CallTo(() => repository.CreateAsync(entity, default!)).Returns(entity);

        var expectedResponse = A.Dummy<CreateReservationResponseDto>();
        // Act
        await sut.HandleAsync(req, default!).ConfigureAwait(false);
        var response = sut.Response;

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<CreateReservationResponseDto>();
        response.Id.Should().Be(expectedResponse.Id);
    }
}
