using Media.Api.Contants;
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

        var clientSideTimezone = TimeZoneInfo.FindSystemTimeZoneById(Constants.ClientSideTimezone);

        var response = reservations.Select(e => new GetReservationsResponseDto(e.Name, e.Device, e.Classroom, TimeZoneInfo.ConvertTimeBySystemTimeZoneId(e.StartDate, clientSideTimezone.Id), TimeZoneInfo.ConvertTimeBySystemTimeZoneId(e.EndDate, clientSideTimezone.Id)));

        await SendOkAsync(response, cancellation: ct).ConfigureAwait(false);
    }
}
