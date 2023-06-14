using OpenAQApiWrapper.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Filters
{
    public abstract class PagingFilter
    {
        [Required]
        [Range(0, 100000)]
        [DefaultValue(100)]
        [JsonPropertyName("limit")]
        public int Limit { get; init; } = 100;

        [Required]
        [Range(0, 6000)]
        [DefaultValue(1)]
        [JsonPropertyName("page")]
        public int Page { get; init; } = 1;

        [Required]
        [DefaultValue(SortOrder.Ascending)]
        [JsonPropertyName("sort")]
        public SortOrder SortBy { get; init; } = SortOrder.Ascending;
    }
}
