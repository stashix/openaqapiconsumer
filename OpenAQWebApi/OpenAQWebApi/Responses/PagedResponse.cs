using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using OpenAQWebApi.Entities;

namespace OpenAQWebApi.Responses
{
    [Serializable]
    [DataContract]
    public class PagedResponse<T> where T : class
    {
        [JsonPropertyName("meta")]
        public Metadata Metadata { get; init; }

        [JsonPropertyName("results")]
        public IList<T> Results { get; init; }
    }
}
