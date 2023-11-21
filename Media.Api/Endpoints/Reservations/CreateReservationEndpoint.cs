using Media.Api.Mappers;
using Media.Api.Repositories;

namespace Media.Api.Endpoints.Reservations;

public sealed class CreateReservationEndpoint : Endpoint<CreateReservationRequestDto, CreateReservationResponseDto, CreateReservationMapper>
{
    private readonly IReservationRepository _repository;

    public CreateReservationEndpoint(IReservationRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Post("/reservations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateReservationRequestDto req, CancellationToken ct)
    {
        _ = req ?? throw new ArgumentNullException(nameof(req));

        var reservation = await Map.ToEntityAsync(req, ct).ConfigureAwait(false);

        var createdReservation = await this._repository
            .CreateAsync(reservation, ct)
            .ConfigureAwait(false);

        var res = await Map.FromEntityAsync(createdReservation, ct).ConfigureAwait(false);

        await SendCreatedAtAsync("/reservations", new { res.Id }, res, cancellation: ct).ConfigureAwait(false);
    }
}
