using Media.Api.Mappers;

namespace Media.Api.Endpoints.Devices;

public sealed class CreateDeviceEndpoint : Endpoint<CreateDeviceRequestDto, CreateDeviceResponseDto, DeviceMapper>
{
    public override void Configure()
    {
        Post("/devices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateDeviceRequestDto req, CancellationToken ct)
    {
        _ = req ?? throw new ArgumentNullException(nameof(req));

        var device = Map.ToEntity(req);

        var response = Map.FromEntity(device);

        await SendCreatedAtAsync<GetDeviceByIdEndpoint>(new { deviceId = response.Id }, response, generateAbsoluteUrl: true, cancellation: ct).ConfigureAwait(false);
    }
}
