using OpenAQApiWrapper.Entities;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Responses
{
    public sealed class PagedResponse<T> where T : class
    {
        [JsonPropertyName("meta")]
        public Metadata Metadata { get; init; } = new Metadata();

        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; init; } = Enumerable.Empty<T>();
    }
}
