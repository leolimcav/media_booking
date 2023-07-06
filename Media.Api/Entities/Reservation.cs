namespace Media.Api.Entities;

public class Reservation : Entity
{
    public string Name { get; set; } = string.Empty;

    public string Device { get; set; } = string.Empty;

    public string Classroom { get; set; } = string.Empty;

    public DateTime Date { get; set; }
}
