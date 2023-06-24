namespace Media.Api.Endpoints.Devices;

public sealed class GetDeviceByIdEndpoint : EndpointWithoutRequest<long>
{
    public override void Configure() 
    {
        Get("/devices/{deviceId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) 
    {
        var deviceId = Route<long>("deviceId");

        await SendAsync(deviceId, cancellation: ct).ConfigureAwait(false);
    }
}
