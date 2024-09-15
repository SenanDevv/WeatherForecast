using Newtonsoft.Json;

namespace Project.WorldWeatherOnlineApi
{
    public sealed class WorldWeatherOnlineTemperature
    {
        [JsonProperty("avgtempC")]
        public double? Temp { get; set; }
    }
}
