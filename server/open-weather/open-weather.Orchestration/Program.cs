var builder = DistributedApplication.CreateBuilder(args);

var redisCache = builder
    .AddRedis("cache", port: 6379)
    .WithImage("redis/redis-stack")
    .WithImageTag("latest")
    .WithLifetime(ContainerLifetime.Persistent);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var postgres = builder
    .AddPostgres("postgres", username, password, 5432)
    .WithDataVolume(isReadOnly: false)
    .WithPgWeb()
    .WithImageTag("latest")
    .WithLifetime(ContainerLifetime.Persistent);

var azurite = builder
    .AddAzureStorage("azurite")
    .RunAsEmulator(
        container =>
            container
                .WithBlobPort(11000)
                .WithQueuePort(11001)
                .WithTablePort(11002)
                .WithDataVolume());

var openweatherDb = postgres.AddDatabase("openweather");

var loadOpenMeteoDataFunction = builder
    .AddAzureFunctionsProject<Projects.OpenWeather_Azure_Function_LoadOpenMeteoData>(
        "openweather-adapters-azure-function-load-open-weather-data")
    .WithExternalHttpEndpoints()
    .WithHostStorage(azurite);

    builder
        .AddProject<Projects.OpenWeather_Adapters_REST>("openweather-adapters-rest")
        .WithReference(redisCache)
        .WithReference(openweatherDb)
        .WithReference(loadOpenMeteoDataFunction)
        .WaitFor(loadOpenMeteoDataFunction);


    
builder.Build().Run();
