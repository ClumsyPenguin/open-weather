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