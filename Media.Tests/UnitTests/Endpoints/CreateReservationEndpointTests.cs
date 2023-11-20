using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Media.Api.Endpoints.Reservations;
using Media.Api.Mappers;
using Media.Api.Repositories;

namespace Media.Tests.UnitTests.Endpoints;

public sealed class CreateReservationEndpointTests
{
    private readonly DateTime date = DateTime.Now;

    [Fact]
    public async Task CreateReservationShouldReturnCreatedReservation()
    {
        // Arrange
        var req = new CreateReservationRequestDto("teste", "teste", "teste", DateOnly.FromDateTime(this.date), TimeOnly.FromDateTime(this.date), TimeOnly.FromDateTime(this.date.AddHours(2)));
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

        var entity = await mapper.ToEntityAsync(req, CancellationToken.None).ConfigureAwait(false);
        
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
