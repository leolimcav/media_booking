namespace Media.Api.Endpoints.Reservations;

public sealed record GetReservationResponseDto(string Name, string Device, string Classroom, DateTime Date);
