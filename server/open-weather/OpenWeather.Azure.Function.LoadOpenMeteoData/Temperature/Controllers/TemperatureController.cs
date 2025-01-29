using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Controllers;

public class TemperatureController(ILogger<TemperatureController> logger, IOpenMeteoService openMeteoService)
{
    private readonly ILogger<TemperatureController> _logger = logger;

    [Function(nameof(GetCurrentTemperature))]
    [OpenApiOperation(operationId: "Run")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(int), Description = "The OK response message containing the current temperature")]
    public async Task<IActionResult> GetCurrentTemperature([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ILogger log)
    {
         var currentTemperatureDto = await openMeteoService.GetCurrentTemperature(1,1);
         
         //TODO: Map DTO and HTTP status codes
         return new OkObjectResult(currentTemperatureDto);
    }
}