using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Core;

namespace Project.WeatherbitApi
{
    public sealed class WeatherbitTemperatureProvider : ITemperatureProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDictionary<string, ThirdPartyConnectionModel> _thirdPartyConnectionModels;

        public WeatherbitTemperatureProvider(
            IHttpClientFactory httpClientFactory,
            IOptions<ThirdPartyConnectionOptions> options
            )
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);
            ArgumentNullException.ThrowIfNull(options);

            _httpClientFactory = httpClientFactory;
            _thirdPartyConnectionModels = options.Value.ThirdPartyConnections;
        }

        public async Task<double?> GetTemperatureDataAsync(DateTime startDate, double latitude, double longitude)
        {
            if (!_thirdPartyConnectionModels.TryGetValue("Weatherbit", out var settings))
            {
                throw new InvalidOperationException("Weatherbit settings not found.");
            }

            var client = _httpClientFactory.CreateClient("WeatherbitClient");

            var requestUrl = $"{settings.BaseUrl}?key={settings.ApiKey}&start_date={startDate:yyyy-MM-dd:HH}&end_date={startDate.AddMonths(1):yyyy-MM-dd:HH}&lat={latitude}&lon={longitude}";

            using var response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var weatherbitResponse = JsonConvert.DeserializeObject<WeatherbitResponse>(jsonResponse) 
                                    ?? throw new InvalidOperationException("Couldn't deserialize WeatherbitResponse response");

            if (weatherbitResponse.Data == null || weatherbitResponse.Data.Count == 0)
            {
                return null;
            }

            // Return the first result. Ignore all others
            return weatherbitResponse.Data.First().Temp;
        }
    }
}
