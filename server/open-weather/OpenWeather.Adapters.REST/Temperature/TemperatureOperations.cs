using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenWeather.Adapters.REST.Temperature.Ports;
using OpenWeather.Domain.Temperature.Models;
using OpenWeather.Domain.Temperature.Validators;

namespace OpenWeather.Adapters.REST.Temperature;

public static class TemperatureOperations
{
    public static async Task<Results<Ok<double>, ValidationProblem>> GetCurrentTemperature(
        [FromQuery(Name = "long")] double longitude, 
        [FromQuery(Name = "lat")] double latitude,
        ITemperatureService temperatureService,
        TemperatureValidator temperatureValidator)
    {
        var request = new GetCurrentTemperatureRequest(longitude, latitude);
        var validation = await temperatureValidator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            IDictionary<string, string[]> errors = validation.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(group => group.Key, group => group.Select(e => e.ErrorMessage).ToArray());
            
            return TypedResults.ValidationProblem(errors);
            
            //TODO this should be an [ValidateRequest] attribute of some sort
        }
        
        var temperature =  await temperatureService.GetCurrentTemperature(request);
        
        return TypedResults.Ok(temperature);
    }
}