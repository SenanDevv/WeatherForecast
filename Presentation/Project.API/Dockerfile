# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/Project.API/Project.API.csproj", "Presentation/Project.API/"]
COPY ["Infrastructure/Project.Geocoding/Project.Geocoding.csproj", "Infrastructure/Project.Geocoding/"]
COPY ["Core/Project.Core/Project.Core.csproj", "Core/Project.Core/"]
COPY ["Infrastructure/Project.Service/Project.Service.csproj", "Infrastructure/Project.Service/"]
COPY ["Infrastructure/Project.FluentValidation/Project.FluentValidation.csproj", "Infrastructure/Project.FluentValidation/"]
COPY ["Infrastructure/Project.WeatherbitApi/Project.WeatherbitApi.csproj", "Infrastructure/Project.WeatherbitApi/"]
COPY ["Infrastructure/Project.WeatherVisualcrossingApi/Project.WeatherVisualcrossingApi.csproj", "Infrastructure/Project.WeatherVisualcrossingApi/"]
COPY ["Infrastructure/Project.WorldWeatherOnline/Project.WorldWeatherOnlineApi.csproj", "Infrastructure/Project.WorldWeatherOnline/"]
RUN dotnet restore "./Presentation/Project.API/Project.API.csproj"
COPY . .
WORKDIR "/src/Presentation/Project.API"
RUN dotnet build "./Project.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Project.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.API.dll"]