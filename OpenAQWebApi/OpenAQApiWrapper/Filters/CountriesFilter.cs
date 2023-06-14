using OpenAQApiWrapper.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Filters
{
    public class CountriesFilter : PagingFilter
    {
        [StringLength(2)]
        [RegularExpression("[A-Za-z][A-Za-z]")]
        [JsonPropertyName("country_id")]
        public string? CountryId { get; init; }

        [JsonPropertyName("country")]
        public IEnumerable<string>? Countries { get; init; }

        [JsonPropertyName("order_by")]
        public CountryOrder? OrderBy { get; init; }
    }
}
