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
        public string? CountryCode { get; init; }

        [JsonPropertyName("coordinates")]
        public Coordinates Coordinates { get; init; } = new Coordinates();

        [JsonPropertyName("measurements")]
        public IList<Measurement> Measurements { get; init; } = Array.Empty<Measurement>();
    }
}
