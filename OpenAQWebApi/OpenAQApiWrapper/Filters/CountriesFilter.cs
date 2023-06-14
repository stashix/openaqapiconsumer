using OpenAQApiWrapper.Entities;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Filters
{
    public sealed class CountriesFilter : PagingFilter
    {
        [JsonPropertyName("country")]
        public IEnumerable<string>? Countries { get; init; }

        [JsonPropertyName("order_by")]
        public CountryOrder? OrderBy { get; init; }
    }
}
