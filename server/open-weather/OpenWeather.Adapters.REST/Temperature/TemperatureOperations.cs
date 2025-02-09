using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenWeather.Domain.Temperature.Models;
using OpenWeather.Domain.Temperature.Services.Ports;
using OpenWeather.Shared.Temperature.Models;

namespace OpenWeather.Adapters.REST.Temperature;

public static class TemperatureOperations
{
    public static async Task<Results<Ok<CurrentTemperature>, ValidationProblem>> GetCurrentTemperature(
        [FromQuery(Name = "long")] double longitude, 
        [FromQuery(Name = "lat")] double latitude,
        ITemperatureService temperatureService,
        IValidator<GetCurrentTemperatureRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new GetCurrentTemperatureRequest(longitude, latitude);
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        //TODO this should be an [ValidateRequest] attribute of some sort
        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var temperature = await temperatureService.GetCurrentTemperatureAsync(request, cancellationToken);
        return TypedResults.Ok(temperature);
    }
}