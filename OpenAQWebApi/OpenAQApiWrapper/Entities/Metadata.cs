using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Entities
{
    public sealed class Metadata
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("license")]
        public string? License { get; init; }

        [JsonPropertyName("website")]
        public string? WebSite { get; init; }

        [JsonPropertyName("limit")]
        public int Limit { get;  init; }

        [JsonPropertyName("page")]
        public int Page { get; init; }

        [JsonPropertyName("found")]
        public int Found { get; init; }
    }
}
