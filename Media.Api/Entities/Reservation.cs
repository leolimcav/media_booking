namespace Media.Api.Entities;

public class Reservation : Entity
{
    public string Name { get; set; } = string.Empty;

    public string Device { get; set; } = string.Empty;

    public string Classroom { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
