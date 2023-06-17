using Media.Api.Entities;

namespace Media.Api.Endpoints.Devices;

public sealed class CreateDeviceEndpoint : Endpoint<CreateDeviceDto>
{
    public override void Configure()
    {
        Post("/api/devices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateDeviceDto req, CancellationToken ct)
    {
        _ = req ?? throw new ArgumentNullException(nameof(req));

        var device = new Device
        {
            Id = 1,
            Name = req.name,
            CreatedBy = "teste",
        };

        await SendCreatedAtAsync("/api/devices", device.Id, HttpStatusCode.Created, true,cancellation: ct).ConfigureAwait(false);
    }
}
