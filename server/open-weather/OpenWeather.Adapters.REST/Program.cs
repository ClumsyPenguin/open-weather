using OpenWeather.Adapters.REST;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("OpenWeather API")
            .WithDarkModeToggle(true)
            .WithDefaultHttpClient(ScalarTarget.Http, ScalarClient.HttpClient)
            .WithTheme(ScalarTheme.Moon);
    });
}

app.UseHttpsRedirection();
app.MapTemperatureEndpoints();
app.UseCors();
app.Run();