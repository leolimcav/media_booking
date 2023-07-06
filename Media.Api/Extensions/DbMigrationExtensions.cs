using Media.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Media.Api.Extensions;

public static class DbMigrationExtensions
{
    public static void RunMigrations(this WebApplication app) 
    {
        _ = app ?? throw new ArgumentNullException(nameof(app));

        using var scope = app.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<MediaDbContext>();

        dbContext.Database.Migrate();
    }
}
