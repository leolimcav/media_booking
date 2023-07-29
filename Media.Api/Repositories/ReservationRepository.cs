using Media.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Media.Api.Repositories;

public sealed class ReservationRepository : IReservationRepository
{
    private readonly MediaDbContext _context;

    public ReservationRepository(MediaDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation> CreateAsync(Reservation entity, CancellationToken cancellationToken = default)
    {
        var createdEntity = await this._context
            .AddAsync<Reservation>(entity, cancellationToken)
            .ConfigureAwait(false);

        await this._context
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return createdEntity.Entity;
    }

    public async Task<IEnumerable<Reservation>> GetReservations(CancellationToken cancellationToken = default)
    {
        return await this._context!
            .Reservations!
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
