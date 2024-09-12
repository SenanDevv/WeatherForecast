using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Project.FluentValidation;
using Project.Service.Services.Abstraction;
using Project.Service.Services.Implementation;

namespace Project.Service.Utilities.DependencyResolvers
{
    public static class ProjectDependencies
    {
        public static void AddProjectDependencies(this IServiceCollection services)
        {
            // Services
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            services.AddTransient<IValidator<ForecastParameters>, ForecastParametersValidator>();

        }
    }
}
