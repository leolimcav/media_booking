using Media.Api.Repositories;

namespace Media.Api.Endpoints.Reservations;

public sealed class GetReservationsEndpoint : EndpointWithoutRequest<IEnumerable<GetReservationsResponseDto>>
{
    private readonly IReservationRepository _repository;

    public GetReservationsEndpoint(IReservationRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Get("/reservations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var reservations = await this._repository
            .GetReservations(ct)
            .ConfigureAwait(false);

        var response = reservations.Select(e => new GetReservationsResponseDto(e.Name, e.Device, e.Classroom, e.StartDate.ToLocalTime(), e.EndDate.ToLocalTime()));
        
        await SendOkAsync(response, cancellation: ct).ConfigureAwait(false);
    }
}
