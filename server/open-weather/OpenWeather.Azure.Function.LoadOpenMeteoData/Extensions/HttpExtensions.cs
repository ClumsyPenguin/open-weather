using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Extensions;

public static class HttpExtensions
{
    public static async Task<IActionResult> HandleRequest<T>(HttpRequest req, IValidator validation,
        Func<T, Task<IActionResult>> handle, bool acceptNull = false)
    {
        var body = await req.GetBodyAsync<T>(validation, acceptNull);
        if (body.IsValid)
        {
            return await handle(body.Value);
        }

        return new BadRequestObjectResult(new { Errors = body.ValidationResults.Select(s => s.ErrorMessage) });
    }
}

public class HttpResponseBody<T>
{
    public bool IsValid { get; set; }
    public T Value { get; set; }

    public IEnumerable<ValidationFailure> ValidationResults { get; set; }
}

public static class HttpRequestExtensions
{
    public static async Task<HttpResponseBody<T>> GetBodyAsync<T>(this HttpRequest request, IValidator validation, bool acceptNull)
    {
        var body = new HttpResponseBody<T>();
        var bodyString = await request.ReadAsStringAsync();
        body.Value = JsonConvert.DeserializeObject<T>(bodyString)!;
        if (!acceptNull && body.Value is null)
        {
            body.IsValid = false;
            body.ValidationResults = new List<ValidationFailure>
            {
                new("Request", "Request cannot be null")
            };
        }
        if(acceptNull)
            body.IsValid = true;
        else
        {
            var context = new ValidationContext<object>(body.Value!);
            var validationResult = await validation.ValidateAsync(context);
            body.IsValid = validationResult.IsValid;
            body.ValidationResults = validationResult.Errors;
        }

        return body;
    }
}