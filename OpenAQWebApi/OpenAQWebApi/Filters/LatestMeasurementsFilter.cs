using OpenAQWebApi.Entities;
using System.Text.Json.Serialization;

namespace OpenAQWebApi.Filters
{
    public class LatestMeasurementsFilter : PagingFilter
    {
        [JsonPropertyName("country")]
        public IEnumerable<string>? Countries { get; init; }

        [JsonPropertyName("city")]
        public IEnumerable<string>? Cities { get; init; }

        [JsonPropertyName("order_by")]
        public MeasurementsOrder? OrderBy { get; init; } 
    }
}
