using Newtonsoft.Json;

namespace Project.WeatherVisualcrossingApi
{
    public sealed class WeatherVisualcrossingResponse
    {
        [JsonProperty("days")]
        public List<WeatherVisualcrossingTemperature> Data { get; set; }
    }
}
