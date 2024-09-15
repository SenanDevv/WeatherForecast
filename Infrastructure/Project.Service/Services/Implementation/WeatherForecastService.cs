using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.Core.Utilities.Results;
using Project.FluentValidation;
using Project.Geocoding;
using Project.Service.Services.Abstraction;

namespace Project.Service.Services.Implementation
{
    public sealed class WeatherForecastService : IWeatherForecastService
    {
        private readonly ILogger _logger;
        private readonly IValidator<ForecastParameters> _validator;
        private readonly IGeocodingService _geocodingService;
        private readonly IEnumerable<ITemperatureProvider> _temperatureProviders;
        private readonly IMemoryCache _cache;
        public WeatherForecastService(
            ILogger<WeatherForecastService> logger,
            IValidator<ForecastParameters> validator,
            IGeocodingService geocodingService,
            IEnumerable<ITemperatureProvider> temperatureProviders,
            IMemoryCache cache
            )
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(validator);
            ArgumentNullException.ThrowIfNull(geocodingService);
            ArgumentNullException.ThrowIfNull(temperatureProviders);
            ArgumentNullException.ThrowIfNull(cache);

            _logger = logger;
            _validator = validator;
            _geocodingService = geocodingService;
            _temperatureProviders = temperatureProviders;
            _cache = cache;
        }

        public async Task<Result> GetForecastsAsync(DateTime? date, string? city, string? country)
        {
            var result = new Result();

            try
            {
                var parameters = new ForecastParameters
                {
                    Date = date,
                    City = city,
                    Country = country
                };

                var validationResult = await _validator.ValidateAsync(parameters);
                if (!validationResult.IsValid)
                {
                    result.Error = string.Join(", ", validationResult.Errors);
                    result.Success = false;
                    return result;
                }

                string cacheKey = $"Forecast_{city}_{country}_{date}";
                if (_cache.TryGetValue(cacheKey, out var cachedResults))
                {
                    result.Success = true;
                    result.Data = cachedResults;
                    return result;
                }

                var geocoding = await _geocodingService.GetGeocodingDataAsync(city!, country!);

                var tasks = _temperatureProviders
                    .Select(tp => ExecuteTemperatureProviderAsync(tp, date!.Value, geocoding.Latitude, geocoding.Longitude));

                var results = await Task.WhenAll(tasks);

                _cache.Set(cacheKey, results, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });

                result.Success = true;
                result.Data = results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                result.Error = "Please contact suport team.";
                result.Success = false;
            }

            return result;
        }

        private static Task<double?> ExecuteTemperatureProviderAsync(
            ITemperatureProvider temperatureProvider,
            DateTime date,
            double latitude,
            double longitude)
        {
            return temperatureProvider.GetTemperatureDataAsync(date, latitude, longitude);
        }
    }
}
