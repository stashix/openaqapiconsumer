using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Entities
{
    public sealed class ValidationError
    {
        [JsonPropertyName("loc")]
        public IList<string> Location { get; init; } = Array.Empty<string>();

        [JsonPropertyName("msg")]
        public string? Message { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }
    }
}
