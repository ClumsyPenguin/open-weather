using FluentValidation;
using OpenWeather.Adapters.REST;
using OpenWeather.Adapters.REST.Temperature;
using OpenWeather.Adapters.REST.Temperature.Ports;
using OpenWeather.Domain.Temperature;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCorsServices();

builder.Services.AddScoped<ITemperatureService, TemperatureService>();
builder.Services.AddValidatorsFromAssembly(OpenWeather.Domain.AssemblyReference.Assembly);

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
app.UseCors("AllowAll");
app.Run();