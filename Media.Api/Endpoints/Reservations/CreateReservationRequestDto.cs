namespace Media.Api.Endpoints.Reservations;

public record CreateReservationRequestDto(string Name, string Device, string Classroom, DateOnly Date, TimeOnly StartTime, TimeOnly EndTime);

