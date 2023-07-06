using Microsoft.EntityFrameworkCore;

namespace Media.Api.Entities;

public sealed class MediaDbContext : DbContext
{
    public MediaDbContext(DbContextOptions<MediaDbContext> options)
        :base(options)
    {    
    }
}
