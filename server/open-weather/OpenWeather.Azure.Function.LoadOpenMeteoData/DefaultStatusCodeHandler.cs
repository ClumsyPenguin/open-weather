using System.Net;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData;

internal class DefaultStatusCodeHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => throw new HttpRequestException("Resource not found (404)."),
            HttpStatusCode.Unauthorized => throw new HttpRequestException("Unauthorized request (401)."),
            _ => response
        };
    }
}
