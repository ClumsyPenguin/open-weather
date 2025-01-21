using FluentValidation;
using OpenWeather.Adapters.Postgres;
using OpenWeather.Adapters.REST.Configuration;
using OpenWeather.Adapters.REST.Temperature;
using OpenWeather.Domain.Temperature.Services;
using OpenWeather.Domain.Temperature.Services.Ports;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.ConfigureCors();
builder.Services.ConfigureCaching(builder.Configuration);
builder.Services.ConfigureDb(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(OpenWeather.Domain.AssemblyReference.Assembly, includeInternalTypes: true);

var app = builder.Build();

app.MapDefaultEndpoints();

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
app.CreateDbIfNotExists();
app.UseHttpsRedirection();
app.MapTemperatureEndpoints();
app.UseCors(Cors.AllowAllPolicy);
app.Run();