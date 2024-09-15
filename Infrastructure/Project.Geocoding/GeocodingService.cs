using Microsoft.Extensions.Options;
using Project.Core;
using System.Text.Json;

namespace Project.Geocoding
{
    public sealed class GeocodingService : IGeocodingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDictionary<string, ThirdPartyConnectionModel> _thirdPartyConnectionModels;

        public GeocodingService(
            IHttpClientFactory httpClientFactory, 
            IOptions<ThirdPartyConnectionOptions> options
            )
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);
            ArgumentNullException.ThrowIfNull(options);

            _httpClientFactory = httpClientFactory;
            _thirdPartyConnectionModels = options.Value.ThirdPartyConnections;
        }

        public async Task<GeocodingModel> GetGeocodingDataAsync(string city, string country)
        {
            if (!_thirdPartyConnectionModels.TryGetValue("Geocoding", out var settings))
            {
                throw new InvalidOperationException("Geocoding settings not found.");
            }

            var client = _httpClientFactory.CreateClient("GeocodingClient");
            var requestUrl = $"{settings.BaseUrl}?city={Uri.EscapeDataString(city)}&country={Uri.EscapeDataString(country)}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("X-Api-Key", settings.ApiKey);

            using var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var results = JsonSerializer.Deserialize<GeocodingModel[]>(jsonResponse);

            if (results == null || results.Length == 0)
            {
                throw new InvalidOperationException("No geocoding found for this city and country");
            }

            // Return the first result. Ignore all others
            return results[0];
        }
    }
}
