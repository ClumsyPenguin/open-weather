var builder = DistributedApplication.CreateBuilder(args);

var redisCache = builder
    .AddRedis("cache", port: 6379)
    .WithImage("redis/redis-stack");
    

builder
    .AddProject<Projects.OpenWeather_Adapters_REST>("openweather-adapters-rest")
    .WithReference(redisCache);

builder.Build().Run();
