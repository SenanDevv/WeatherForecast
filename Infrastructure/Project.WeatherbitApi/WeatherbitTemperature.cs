using Newtonsoft.Json;

namespace Project.WeatherbitApi
{
    public sealed class WeatherbitTemperature
    {
        [JsonProperty("temp")]
        public double? Temp { get; set; }
    }
}
