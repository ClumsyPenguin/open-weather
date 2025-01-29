using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeather.Adapters.Postgres.Models;

namespace OpenWeather.Adapters.Postgres;

internal static class DbInitializer
{
    internal static void Initialize(OpenWeatherDbContext context)
    {
        var databaseIsCreated = context.Database.EnsureCreated();

        if (!databaseIsCreated)
            return;

        if (context.Temperatures.Any())
            return;

        SeedData(context);
    }

    private static void SeedData(OpenWeatherDbContext context)
    {
        var temperatures = new List<Temperature>
        {
            new() { Latitude = 51.21, Longitude = 4.39, TemperatureC = 21, TimeStamp = DateTime.UtcNow }
        };

        context.Temperatures.AddRange(temperatures);

        context.SaveChanges();
    }
}