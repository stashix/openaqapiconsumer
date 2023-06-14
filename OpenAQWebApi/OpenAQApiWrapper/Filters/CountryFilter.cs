using OpenAQApiWrapper.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Filters
{
    public sealed class CountryFilter : PagingFilter
    {
        [Required]
        [StringLength(2)]
        [RegularExpression("[A-Za-z][A-Za-z]")]
        [JsonPropertyName("country_id")]
        public string? CountryId { get; init; }

        [JsonPropertyName("order_by")]
        public CountryOrder? OrderBy { get; init; }
    }
}
