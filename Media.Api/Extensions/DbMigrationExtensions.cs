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

        var pendingMigrations = dbContext.Database.GetPendingMigrations();

        if (!pendingMigrations.Any())
        {
            var loggerMessage = LoggerMessage.Define<int>(LogLevel.Information, 1, "{MigrationsNumber} Migrations Pending!");

            loggerMessage(app.Logger, 0, null);
            return;
        }

        dbContext.Database.Migrate();
    }
}
