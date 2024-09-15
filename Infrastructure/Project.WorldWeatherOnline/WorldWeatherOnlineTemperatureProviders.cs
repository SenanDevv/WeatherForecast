using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Core;

namespace Project.WorldWeatherOnlineApi
{
    public sealed class WorldWeatherOnlineTemperatureProviders : ITemperatureProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDictionary<string, ThirdPartyConnectionModel> _thirdPartyConnectionModels;

        public WorldWeatherOnlineTemperatureProviders(
            IHttpClientFactory httpClientFactory,
            IOptions<ThirdPartyConnectionOptions> options
            )
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);
            ArgumentNullException.ThrowIfNull(options);

            _httpClientFactory = httpClientFactory;
            _thirdPartyConnectionModels = options.Value.ThirdPartyConnections;
        }

        public async Task<double?> GetTemperatureDataAsync(DateTime date, double latitude, double longitude)
        {
            if (!_thirdPartyConnectionModels.TryGetValue("WorldWeatherOnline", out var settings))
            {
                throw new InvalidOperationException("WeatherVisualcrossing settings not found.");
            }

            var client = _httpClientFactory.CreateClient("WorldWeatherOnlineClient");

            var requestUrl = $"{settings.BaseUrl}?key={settings.ApiKey}&q={latitude},{longitude}&date={date:yyyy-MM-dd}&format=json";

            using var response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var weatherVisualcrossingResponse = JsonConvert.DeserializeObject<WorldWeatherOnlineResponse>(jsonResponse)
                                             ?? throw new InvalidOperationException("Couldn't deserialize WorldWeatherOnline response");

            var data = weatherVisualcrossingResponse.Data;

            if (data == null || data.WeatherModels == null  || data.WeatherModels.Count == 0)
            {
                return null;
            }

            return data.WeatherModels.First().Temp;
        }
    }
}
