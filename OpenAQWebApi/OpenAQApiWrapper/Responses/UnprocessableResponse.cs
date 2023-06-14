using OpenAQApiWrapper.Entities;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Responses
{
    public sealed class UnprocessableResponse
    {
        [JsonPropertyName("detail")]
        public IList<ValidationError> Errors { get; init; } = Array.Empty<ValidationError>();
    }
}
