using System.Text.Json.Serialization;

namespace Project.Geocoding
{
    public sealed class GeocodingModel
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
