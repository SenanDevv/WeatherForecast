using Newtonsoft.Json;

namespace Project.WeatherVisualcrossingApi
{
    public sealed class WeatherVisualcrossingTemperature
    {
        [JsonProperty("temp")]
        public double? Temp { get; set; }
    }
}
