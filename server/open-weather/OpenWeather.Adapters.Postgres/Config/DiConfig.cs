using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpenWeather.Adapters.Postgres.Config;

public static class DiConfig
{
    public static void Configure(IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection.AddNpgsql<OpenWeatherDbContext>(config.GetConnectionString("OpenWeatherDB"));
    }
}