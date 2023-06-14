using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Entities
{
    public sealed class Measurement
    {
        [JsonPropertyName("parameter")]
        public string? Parameter { get; init; }

        [JsonPropertyName("value")]
        public double Value { get; init; }

        [JsonPropertyName("unit")]
        public string? Unit { get; init; }

        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; init; }
    }
}
