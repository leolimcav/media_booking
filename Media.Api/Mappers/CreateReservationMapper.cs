using Media.Api.Endpoints.Reservations;
using Media.Api.Entities;

namespace Media.Api.Mappers;

public sealed class CreateReservationMapper : Mapper<CreateReservationRequestDto, CreateReservationResponseDto, Reservation>
{
    public override Task<CreateReservationResponseDto> FromEntityAsync(Reservation e, CancellationToken ct = default!)
    {
        _ = e ?? throw new ArgumentNullException(nameof(e));

        return Task.FromResult(new CreateReservationResponseDto(e.Id, e.Name, e.Device, e.Classroom, e.StartDate, e.EndDate, e.CreatedAt));
    }

    public override Task<Reservation> ToEntityAsync(CreateReservationRequestDto r, CancellationToken ct = default!)
    {
        _ = r ?? throw new ArgumentNullException(nameof(r));

        return Task.FromResult(new Reservation 
        {
            Name = r.Name,
            Device = r.Device,
            Classroom = r.Classroom,
            StartDate = r.StartDate.ToUniversalTime(),
            EndDate = r.EndDate.ToUniversalTime()
        });
    }
}
