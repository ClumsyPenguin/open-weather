using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Controllers;

public class TemperatureController(ILogger<TemperatureController> logger)
{
    private readonly ILogger<TemperatureController> _logger = logger;

    [Function(nameof(GetCurrentTemperature))]
    [OpenApiOperation(operationId: "Run")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(int), Description = "The OK response message containing the current temperature")]
    public IActionResult GetCurrentTemperature([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,ILogger log)
    {
        return new OkObjectResult(1);
    }
}