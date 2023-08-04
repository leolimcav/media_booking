using Media.Api.Repositories;

namespace Media.Api.Extensions;

public static class AddRepositoriesExtensions
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddScoped<IReservationRepository, ReservationRepository>();
    }
}
