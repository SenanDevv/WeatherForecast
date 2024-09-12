using Project.Core.Utilities.Results;

namespace Project.Service.Services.Abstraction
{
    public interface IWeatherForecastService
    {
        Task<Result> GetForecasts(DateTime? date, string? city, string? country);
    }
}
