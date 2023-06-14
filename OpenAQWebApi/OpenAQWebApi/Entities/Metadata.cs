using System.Text.Json.Serialization;

namespace OpenAQWebApi.Entities
{
    public class Metadata
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
        public long Found { get; init; }
    }
}
