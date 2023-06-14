using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Entities
{
    public sealed class LocationMeasurements
    {
        [JsonPropertyName("location")]
        public string? Location { get; init; }

        [JsonPropertyName("city")]
        public string? City { get; init; }

        [JsonPropertyName("country")]
        public string? Country { get; init; }

        [JsonPropertyName("coordinates")]
        public GeoCoordinates? Coordinates { get; init; }

        [JsonPropertyName("measurements")]
        public IList<Measurement> Measurements { get; init; } = Array.Empty<Measurement>();
    }
}
