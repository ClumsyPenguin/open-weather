using Microsoft.AspNetCore.Http.HttpResults;
using OpenWeather.Adapters.REST.Temperature.Ports;

namespace OpenWeather.Adapters.REST.Temperature;

public static partial class ApiMapper
{
    public static WebApplication MapTemperatureEndpoints(this WebApplication app)
    {
        var temperatureItems = app.MapGroup("/temperature");
        
        temperatureItems.MapGet("/current-temperature", TemperatureOperations.GetCurrentTemperature)
            .WithName("GetCurrentTemperature")
            .WithSummary("Get current temperature")
            .WithDescription("Get the current temperature at your current location");

        return app;
    }
}

public static class TemperatureOperations
{
    public static async Task<Results<Ok<double>, UnprocessableEntity, NotFound>> GetCurrentTemperature(ITemperatureService temperatureService)
    {
        var temperature =  await temperatureService.GetCurrentTemperature();
        
        return TypedResults.Ok(temperature);
    }
}