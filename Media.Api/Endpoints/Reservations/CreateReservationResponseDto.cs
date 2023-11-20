namespace Media.Api.Endpoints.Reservations;

public record CreateReservationResponseDto(long Id, string Name, string Device, string Classroom, DateOnly Date, TimeOnly StartTime, TimeOnly EndTime, DateTime CreatedAt);
