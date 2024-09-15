using Microsoft.Extensions.DependencyInjection;
using Project.Core;

namespace Project.WorldWeatherOnlineApi.Utilities.DependencyResolvers
{
    public static class ProjectDependencies
    {
        public static void AddWorldWeatherOnline(this IServiceCollection services)
        {
            services.AddHttpClient("WorldWeatherOnlineClient", client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Project/1.0");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<ITemperatureProvider, WorldWeatherOnlineTemperatureProviders>();
        }
    }
}
