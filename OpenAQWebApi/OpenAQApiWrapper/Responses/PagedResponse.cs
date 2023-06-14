using OpenAQApiWrapper.Entities;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Responses
{
    public sealed class PagedResponse<T> where T : class
    {
        [JsonPropertyName("meta")]
        public Metadata Metadata { get; init; } = new Metadata();

        [JsonPropertyName("results")]
        public IList<T> Results { get; init; } = Array.Empty<T>();
    }
}
