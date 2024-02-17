namespace Media.Api.Endpoints.Reservations;

public record CreateReservationRequestDto(string Name, string Device, string Classroom, DateTime StartDate, DateTime EndDate);

