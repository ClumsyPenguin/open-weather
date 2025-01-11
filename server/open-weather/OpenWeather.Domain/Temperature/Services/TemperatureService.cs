using Microsoft.Extensions.Caching.Hybrid;
using OpenWeather.Domain.Temperature.Models;
using OpenWeather.Domain.Temperature.Services.Ports;

namespace OpenWeather.Domain.Temperature.Services;

public class TemperatureService(HybridCache hybridCache) : ITemperatureService
{
    public async Task<double> GetCurrentTemperatureAsync(GetCurrentTemperatureRequest request,
        CancellationToken cancellationToken)
    {
        return await hybridCache.GetOrCreateAsync(
            $"temperature-{request.Longitude}-{request.Latitude}",
            async _ => await Task.FromResult(21d),
            tags: ["temperature"],
            cancellationToken: cancellationToken
        );
    }
}