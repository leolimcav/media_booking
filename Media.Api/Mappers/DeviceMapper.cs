using System.Security.Cryptography;
using Media.Api.Endpoints.Devices;
using Media.Api.Entities;

namespace Media.Api.Mappers;

public sealed class DeviceMapper : Mapper<CreateDeviceRequestDto, CreateDeviceResponseDto, Device>
{
  public override Device ToEntity(CreateDeviceRequestDto r) 
  {
    _ = r ?? throw new ArgumentNullException(nameof(r));

    return new Device 
    {
      Id = RandomNumberGenerator.GetInt32(100),
      Name = r.Name,
      CreatedBy = "Teste"
    };
  }

  public override CreateDeviceResponseDto FromEntity(Device e)
  {
    _ = e ?? throw new ArgumentNullException(nameof(e));

    return new CreateDeviceResponseDto
    (
      e.Id,
      e.Name,
      e.CreatedBy,
      e.CreatedAt,
      e.UpdatedAt
    );
  }
}
