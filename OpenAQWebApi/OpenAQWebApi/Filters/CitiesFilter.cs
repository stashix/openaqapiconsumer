using OpenAQWebApi.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAQWebApi.Filters
{
    public class CitiesFilter : PagingFilter
    {
        [StringLength(2)]
        [RegularExpression("[a-Z][a-Z]")]
        [JsonPropertyName("country_id")]
        public string? CountryId { get; init; }

        [JsonPropertyName("country")]
        public IEnumerable<string>? Countries { get; init; }

        [JsonPropertyName("city")]
        public IEnumerable<string>? Cities { get; init; }

        [JsonPropertyName("order_by")]
        public Order OrderBy { get; init; }
    }
}
