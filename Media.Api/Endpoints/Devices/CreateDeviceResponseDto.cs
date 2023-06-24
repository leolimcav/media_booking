namespace Media.Api.Endpoints.Devices
{
  public sealed record CreateDeviceResponseDto(
    long Id,
    string Name,
    string CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt);
}