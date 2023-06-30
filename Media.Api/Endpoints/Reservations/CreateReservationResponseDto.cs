namespace Media.Api.Endpoints.Reservations;

public record CreateReservationResponseDto(long Id, string Name, string Device, string Classroom, DateTime Date, DateTime CreatedAt);
