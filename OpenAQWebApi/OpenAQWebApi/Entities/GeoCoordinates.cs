using System.Text.Json.Serialization;

namespace OpenAQWebApi.Entities
{
    public class GeoCoordinates
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; init; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; init; }
    }
}
