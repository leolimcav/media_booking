using Media.Api.Mappers;

namespace Media.Api.Endpoints.Reservations;

public sealed class CreateReservationEndpoint : Endpoint<CreateReservationRequestDto, CreateReservationResponseDto, ReservationMapper>
{
    public override void Configure()
    {
        Post("/reservations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateReservationRequestDto req, CancellationToken ct)
    {
        _ = req ?? throw new ArgumentNullException(nameof(req));

        var reservation = Map.ToEntity(req);

        var res = Map.FromEntity(reservation);

        await SendCreatedAtAsync("/reservations", new { res.Id }, res, cancellation: ct).ConfigureAwait(false);
    }
}
