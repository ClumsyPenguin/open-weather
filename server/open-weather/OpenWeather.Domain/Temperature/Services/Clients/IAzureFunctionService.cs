using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Services.Clients;

public interface IAzureFunctionService
{
    public Task<CurrentTemperature> GetCurrentTemperatureAsync(double latitude, double longitude);
}