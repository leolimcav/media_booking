using Media.Api.Endpoints.Reservations;
using Media.Api.Entities;

namespace Media.Api.Mappers;

public sealed class ReservationMapper : Mapper<CreateReservationRequestDto, CreateReservationResponseDto, Reservation>
{
    public override CreateReservationResponseDto FromEntity(Reservation e)
    {
        _ = e ?? throw new ArgumentNullException(nameof(e));

        return new CreateReservationResponseDto(1, e.Name, e.Device, e.Classroom, e.Date, e.CreatedAt);
    }

    public override Reservation ToEntity(CreateReservationRequestDto r)
    {
        _ = r ?? throw new ArgumentNullException(nameof(r));

        return new Reservation 
        {
            Id = 1,
            Name = r.Name,
            Device = r.Device,
            Classroom = r.Classroom,
            Date = r.Date,
        };
    }
}
