using System.Net;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Validators;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Controllers;

public class TemperatureController(ILogger<TemperatureController> logger, IOpenMeteoService openMeteoService, CoordinatesValidator validation)
{
    private readonly ILogger<TemperatureController> _logger = logger;
    
    [Function(nameof(GetCurrentTemperature))]
    [OpenApiOperation(operationId: "Run")]
    [OpenApiParameter("latitude", In = ParameterLocation.Query, Type = typeof(double), Required = true, Description = "the latitude value of your requested location")]
    [OpenApiParameter("longitude", In = ParameterLocation.Query, Type = typeof(double), Required = true, Description = "the longitude value of your requested location")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetCurrentTemperatureDTO), Description = "The OK response message containing the current temperature")]
    public async Task<Results<Ok<GetCurrentTemperatureDTO>, ValidationProblem>> GetCurrentTemperature(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-current-temperature")]
        HttpRequest req, 
        double latitude,
        double longitude,
        ILogger log)
    {
        var request = new GetCurrentTemperatureRequest(longitude, latitude);
        var validationResult = await validation.ValidateAsync(request);
        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        
        var result = await openMeteoService.GetCurrentTemperature(request);
        //TODO: Map DTO and HTTP status codes

       return TypedResults.Ok(result);
    }
}