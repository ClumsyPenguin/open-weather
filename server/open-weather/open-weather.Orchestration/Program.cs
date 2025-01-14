var builder = DistributedApplication.CreateBuilder(args);

var redisCache = builder
    .AddRedis("cache", port: 6379)
    .WithImage("redis/redis-stack")
    .WithImageTag("latest");

var postGreSqlDb = builder
    .AddPostgres("db")
    .WithPgAdmin()
    .WithImageTag("latest");

builder
    .AddProject<Projects.OpenWeather_Adapters_REST>("openweather-adapters-rest")
    .WithReference(redisCache)
    .WithReference(postGreSqlDb);

builder.Build().Run();
