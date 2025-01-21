using OpenWeather.Adapters.REST.Configuration;
using OpenWeather.Adapters.REST.Temperature;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.ConfigureCors();
builder.Services.ConfigureCaching(builder.Configuration);

ConfigureDependencies(builder);

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

app.UseHttpsRedirection();
app.MapTemperatureEndpoints();
app.UseCors(Cors.AllowAllPolicy);
app.Run();

void ConfigureDependencies(WebApplicationBuilder builder)
{
    OpenWeather.Adapters.Postgres.Config.DbContextConfig.Configure(builder.Services, builder.Configuration);
    OpenWeather.Domain.Config.DiConfig.Configure(builder.Services);
}