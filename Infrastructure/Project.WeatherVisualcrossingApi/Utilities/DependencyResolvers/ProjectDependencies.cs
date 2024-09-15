﻿using Microsoft.Extensions.DependencyInjection;
using Project.Core;

namespace Project.WeatherVisualcrossingApi.Utilities.DependencyResolvers
{
    public static class ProjectDependencies
    {
        public static void AddWeatherVisualcrossing(this IServiceCollection services)
        {
            services.AddHttpClient("WeatherVisualcrossingClient", client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Project/1.0");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<ITemperatureProvider, WeatherVisualcrossingTemperatureProvider>();
        }
    }
}
