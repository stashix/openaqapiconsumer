using OpenAQApiWrapper.Converters;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Entities
{
    public class Country
    {
        [JsonPropertyName("code")]
        public string? Code { get; init; }

        [JsonPropertyName("name")]
        public string? Name { get; init; }

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

        [JsonPropertyName("count")]
        public int Count { get; init; }

        [JsonPropertyName("cities")]
        public int Cities { get; init; }

        [JsonPropertyName("sources")]
        public int Source { get; init; }
    }
}
