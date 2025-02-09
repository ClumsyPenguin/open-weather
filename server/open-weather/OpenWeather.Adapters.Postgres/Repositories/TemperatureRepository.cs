using Microsoft.EntityFrameworkCore;
using OpenWeather.Domain.Temperature.Ports;
using OpenWeather.Shared.Temperature.Models;

namespace OpenWeather.Adapters.Postgres.Repositories;

public class TemperatureRepository(OpenWeatherDbContext dbContext) : ITemperatureRepository
{
    private const double Tolerance = 0.01;

    public async Task<CurrentTemperature?> GetCurrentTemperatureAsync(double latitude, double longitude)
    {
        var temperatureEntity = await dbContext.Temperatures
            .Where(x => Math.Abs(x.Longitude - longitude) < Tolerance && Math.Abs(x.Latitude - latitude) < Tolerance)
            .FirstOrDefaultAsync();

        if (temperatureEntity is null)
            return null;
        
        return new CurrentTemperature
        {
            Longitude = temperatureEntity.Longitude,
            Latitude = temperatureEntity.Latitude,
            TemperatureC = temperatureEntity.TemperatureC,
            TimeStamp = temperatureEntity.TimeStamp
        };
    }
}