using OpenWeather.Adapters.Postgres.Models;

namespace OpenWeather.Adapters.Postgres;

public static class DbInitializer
{
    public static void Initialize(OpenWeatherDbContext context)
    {
        if(context.Temperatures.Any())
            return;

        var temperatures = new List<Temperature>
        {
            new() { Latitude = 51.21, Longitude = 4.39, TemperatureC = 21, TimeStamp = DateTime.UtcNow }
        };
        
        context.Temperatures.AddRange(temperatures);

        context.SaveChanges();
    }
}