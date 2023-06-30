using Media.Api.Endpoints.Devices;
using Media.Api.Mappers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Media.Tests.UnitTests.Endpoints;

public sealed class CreateDeviceEndpointTests
{
    [Fact]
    public async Task CreatingDeviceWithValidDtoShouldReturnCreatedDevice()
    {
        // Arrange
        var req = new CreateDeviceRequestDto("Teste1");
        var linkGen = A.Dummy<LinkGenerator>();

        var sut = Factory.Create<CreateDeviceEndpoint>(ctx =>
                {
                    ctx.AddTestServices(s =>
                           {
                               s.AddSingleton(linkGen);
                               s.BuildServiceProvider();
                           });
                });

        sut.Map = new DeviceMapper();

        // Act
        await sut.HandleAsync(req, default!).ConfigureAwait(false);

        var response = sut.Response;

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<CreateDeviceResponseDto>();
        response.Name.Should().Be(req.Name);
    }
}
