namespace Media.Api.Endpoints.Reservations;

public sealed record GetReservationsResponseDto(string Name, string Device, string Classroom, DateTime Date);
