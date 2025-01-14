var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.OpenWeather_Adapters_REST>("openweather-adapters-rest");

builder.Build().Run();
