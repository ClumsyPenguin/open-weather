using Microsoft.AspNetCore.Http.HttpResults;

namespace OpenWeather.Adapters.REST;

public static partial class ApiMapper
{
    public static WebApplication MapTemperatureEndpoints(this WebApplication app)
    {
        var temperatureItems = app.MapGroup("/temperature")
            .RequireCors("AllowAll");

        temperatureItems.MapGet("/current-temperature", TemperatureOperations.GetCurrentTemperature)
            .WithName("GetCurrentTemperature")
            .WithSummary("Get current temperature")
            .WithDescription("Get the current temperature at your current location");

        return app;
    }
}

public static class TemperatureOperations
{
    public static Task<Results<Ok<int>, UnprocessableEntity>> GetCurrentTemperature()
    {
        return Task.FromResult<Results<Ok<int>, UnprocessableEntity>>(TypedResults.Ok(1));
    }
}