namespace Project.Geocoding
{
    public interface IGeocodingService
    {
        Task<GeocodingModel> GetGeocodingDataAsync(string city, string country);
    }
}
