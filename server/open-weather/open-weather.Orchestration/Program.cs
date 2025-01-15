var builder = DistributedApplication.CreateBuilder(args);

var redisCache = builder
    .AddRedis("cache", port: 6379)
    .WithImage("redis/redis-stack")
    .WithImageTag("latest");

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var postgres = builder
    .AddPostgres("postgres", username, password, 5432)
    .WithDataVolume(isReadOnly: false)
    .WithPgWeb()
    .WithImageTag("latest");

var openweatherDb = postgres.AddDatabase("openweather");

builder
    .AddProject<Projects.OpenWeather_Adapters_REST>("openweather-adapters-rest")
    .WithReference(redisCache)
    .WithReference(openweatherDb);

builder.Build().Run();
