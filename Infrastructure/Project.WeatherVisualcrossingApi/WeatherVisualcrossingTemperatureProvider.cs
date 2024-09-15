using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Core;
using Project.Core.Utilities;

namespace Project.WeatherVisualcrossingApi
{
    public sealed class WeatherVisualcrossingTemperatureProvider : ITemperatureProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDictionary<string, ThirdPartyConnectionModel> _thirdPartyConnectionModels;

        public WeatherVisualcrossingTemperatureProvider(
            IHttpClientFactory httpClientFactory,
            IOptions<ThirdPartyConnectionOptions> options
            )
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);
            ArgumentNullException.ThrowIfNull(options);

            _httpClientFactory = httpClientFactory;
            _thirdPartyConnectionModels = options.Value.ThirdPartyConnections;
        }

        public async Task<double?> GetTemperatureDataAsync(DateTime dateTime, double latitude, double longitude)
        {
            if (!_thirdPartyConnectionModels.TryGetValue("WeatherVisualcrossing", out var settings))
            {
                throw new InvalidOperationException("WeatherVisualcrossing settings not found.");
            }

            var client = _httpClientFactory.CreateClient("WeatherVisualcrossingClient");

            var requestUrl = $"{settings.BaseUrl}/{latitude},{longitude}/{dateTime:yyyy-MM-dd}/?key={settings.ApiKey}&elements=temp,datetime&timezone=UTC";

            using var response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var weatherVisualcrossingResponse = JsonConvert.DeserializeObject<WeatherVisualcrossingResponse>(jsonResponse)
                                    ?? throw new InvalidOperationException("Couldn't deserialize WeatherVisualcrossingResponse response");

            if (weatherVisualcrossingResponse.Data == null || weatherVisualcrossingResponse.Data.Count == 0)
            {
                return null;
            }

            var tempInFahrenheit = weatherVisualcrossingResponse.Data.First().Temp;

            if (tempInFahrenheit == null)
            {
                return null;
            }

            return TemperatureConverter.ConvertFahrenheitToCelsius(tempInFahrenheit.Value);
        }
    }
}
