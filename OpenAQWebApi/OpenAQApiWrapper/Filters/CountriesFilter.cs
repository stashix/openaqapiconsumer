using OpenAQApiWrapper.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Filters
{
    public sealed class CountriesFilter : PagingFilter
    {
        public CountriesFilter()
        {
            Countries = Enumerable.Empty<string>();
        }

        [StringLength(2)]
        [RegularExpression("[A-Za-z][A-Za-z]")]
        [JsonPropertyName("country_id")]
        public string? CountryId { get; init; }

        [JsonPropertyName("country")]
        public IEnumerable<string> Countries { get; init; }

        [JsonPropertyName("order_by")]
        public CountryOrder? OrderBy { get; init; }
    }
}
