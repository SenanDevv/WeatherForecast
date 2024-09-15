using Newtonsoft.Json;

namespace Project.WeatherbitApi
{
    public sealed class WeatherbitResponse
    {
        [JsonProperty("data")]
        public List<WeatherbitTemperature> Data { get; set; }
    }
}
