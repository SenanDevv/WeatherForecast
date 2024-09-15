using Newtonsoft.Json;

namespace Project.WorldWeatherOnlineApi
{
    public sealed class WorldWeatherOnlineResponse
    {
        [JsonProperty("data")]
        public WorldWeatherOnlineData Data { get; set; }
    }
}
