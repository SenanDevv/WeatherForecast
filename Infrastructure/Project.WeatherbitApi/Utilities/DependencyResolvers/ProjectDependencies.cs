using Microsoft.Extensions.DependencyInjection;
using Project.Core;

namespace Project.WeatherbitApi.Utilities.DependencyResolvers
{
    public static class ProjectDependencies
    {
        public static void AddWeatherbitApi(this IServiceCollection services)
        {
            services.AddHttpClient("WeatherbitClient", client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Project/1.0");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<ITemperatureProvider, WeatherbitTemperatureProvider>();
        }
    }
}
