using Microsoft.Extensions.DependencyInjection;

namespace Project.Geocoding.Utilities.DependencyResolvers
{
    public static class ProjectDependencies
    {
        public static void AddGeocoding(this IServiceCollection services)
        {
            services.AddHttpClient("GeocodingClient", client =>
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Project/1.0");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddScoped<IGeocodingService, GeocodingService>();
        }
    }
}
