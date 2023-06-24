namespace Media.Api.Entities;

public sealed class Device : Entity
{
    public string Name { get; set; } = string.Empty;

    public string CreatedBy { get; set; } = string.Empty;
}
