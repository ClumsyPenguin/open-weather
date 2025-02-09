using OpenWeather.Domain.Temperature.Models;
using OpenWeather.Shared.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Services.Ports;

public interface ITemperatureService
{
    public Task<CurrentTemperature> GetCurrentTemperatureAsync(GetCurrentTemperatureRequest request, CancellationToken cancellationToken);
}