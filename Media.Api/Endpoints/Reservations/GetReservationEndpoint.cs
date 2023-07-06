namespace Media.Api.Endpoints.Reservations;

public sealed class GetReservationEndpoint : EndpointWithoutRequest<IEnumerable<GetReservationResponseDto>>
{
    public override void Configure()
    {
        Get("/reservations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<GetReservationResponseDto> response = new List<GetReservationResponseDto>();

        await SendOkAsync(response, cancellation: ct).ConfigureAwait(false);
    }
}
