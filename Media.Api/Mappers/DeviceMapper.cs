using System.Security.Cryptography;
using Media.Api.Endpoints.Devices;
using Media.Api.Entities;

namespace Media.Api.Mappers;

public sealed class DeviceMapper : Mapper<CreateDeviceRequestDto, CreateDeviceResponseDto, Device>
{
  public override Task<Device> ToEntityAsync(CreateDeviceRequestDto r, CancellationToken ct = default!) 
  {
    _ = r ?? throw new ArgumentNullException(nameof(r));

    return Task.FromResult(new Device 
    {
      Id = RandomNumberGenerator.GetInt32(100),
      Name = r.Name,
      CreatedBy = "Teste"
    });
  }

  public override Task<CreateDeviceResponseDto> FromEntityAsync(Device e, CancellationToken ct = default!)
  {
    _ = e ?? throw new ArgumentNullException(nameof(e));

    return Task.FromResult(new CreateDeviceResponseDto
    (
      e.Id,
      e.Name,
      e.CreatedBy,
      e.CreatedAt,
      e.UpdatedAt
    ));
  }
}
