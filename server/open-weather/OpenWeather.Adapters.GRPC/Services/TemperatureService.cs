using Grpc.Core;
using OpenWeather.ServiceEndpoints;

namespace OpenWeather.Adapter.GRPC.Services;

public class TemperatureService : OpenWeather.ServiceEndpoints.TemperatureService.TemperatureServiceBase
{
    public override Task<TemperatureResponse> GetTemperature(GetTemperatureQuery query, ServerCallContext context)
    {
        return Task.FromResult(new TemperatureResponse()
        { 
            Temperature = 1d
        });
    }
}