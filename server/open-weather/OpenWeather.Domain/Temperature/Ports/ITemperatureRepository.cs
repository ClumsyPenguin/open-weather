using OpenWeather.Shared.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Ports;

public interface ITemperatureRepository
{
    public Task<CurrentTemperature?> GetCurrentTemperatureAsync(double latitude, double longitude);
}