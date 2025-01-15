using OpenWeather.Adapters.Postgres;

namespace OpenWeather.Adapters.REST.Configuration;

public static class Database
{
    public static IServiceCollection ConfigureDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddNpgsql<OpenWeatherDbContext>(configuration.GetConnectionString("OpenWeatherDB"));
        
        return services;
    }
}