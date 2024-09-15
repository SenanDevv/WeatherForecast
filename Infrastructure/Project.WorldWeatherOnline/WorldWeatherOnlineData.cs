using Newtonsoft.Json;

namespace Project.WorldWeatherOnlineApi
{
    public sealed class WorldWeatherOnlineData
    {
        [JsonProperty("weather")]
        public List<WorldWeatherOnlineTemperature> WeatherModels { get; set; }
    }
}
