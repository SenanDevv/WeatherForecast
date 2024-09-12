using FluentValidation;
using Microsoft.Extensions.Logging;
using Project.Core.Utilities.Results;
using Project.FluentValidation;
using Project.Service.Services.Abstraction;

namespace Project.Service.Services.Implementation
{
    public sealed class WeatherForecastService : IWeatherForecastService
    {
        private readonly ILogger _logger;
        private readonly IValidator<ForecastParameters> _validator;

        public WeatherForecastService(
            ILogger<WeatherForecastService> logger, 
            IValidator<ForecastParameters> validator
            )
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result> GetForecasts(DateTime? date, string? city, string? country)
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                result.Error = "Please contact suport team.";
                result.Success = false;
            }

            return result;
        }
    }
}
