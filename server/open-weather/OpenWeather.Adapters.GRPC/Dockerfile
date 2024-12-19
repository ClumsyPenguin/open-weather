FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OpenWeather.Adapter.GRPC/OpenWeather.Adapter.GRPC.csproj", "OpenWeather.Adapter.GRPC/"]
RUN dotnet restore "OpenWeather.Adapter.GRPC/OpenWeather.Adapter.GRPC.csproj"
COPY . .
WORKDIR "/src/OpenWeather.Adapter.GRPC"
RUN dotnet build "OpenWeather.Adapter.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OpenWeather.Adapter.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenWeather.Adapter.GRPC.dll"]
