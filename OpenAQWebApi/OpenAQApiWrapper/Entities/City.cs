using OpenAQApiWrapper.Converters;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Entities
{
    public class City
    {
        [JsonPropertyName("country")]
        public string? CountryCode { get; init; }

        [JsonPropertyName("city")]
        public string? Name { get; init; }

        [JsonPropertyName("count")]
        public int Count { get; init; }

        [JsonPropertyName("locations")]
        public int Locations { get; init; }

        [JsonPropertyName("firstUpdated")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTime? FirstUpdated { get; init; }

        [JsonPropertyName("lastUpdated")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTime? LastUpdated { get; init; }

        [JsonPropertyName("parameters")]
        public IList<string>? Parameters { get; init; }
    }
}
