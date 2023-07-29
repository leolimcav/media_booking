using Media.Api.Entities;

namespace Media.Api.Repositories;

public interface IReservationRepository
{
    Task<Reservation> CreateAsync(Reservation entity, CancellationToken cancellationToken = default!);

    Task<IEnumerable<Reservation>> GetReservations(CancellationToken cancellationToken = default!);
}
