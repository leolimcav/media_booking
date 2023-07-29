using Microsoft.EntityFrameworkCore;

namespace Media.Api.Entities;

public class MediaDbContext : DbContext
{
    public MediaDbContext(DbContextOptions<MediaDbContext> options)
        :base(options)
    {    
    }

    public DbSet<Reservation>? Reservations { get; set; }
}
