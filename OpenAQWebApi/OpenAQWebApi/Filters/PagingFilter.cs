using OpenAQWebApi.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAQWebApi.Filters
{
    public abstract class PagingFilter
    {
        [Required]
        [Range(0, 100000)]
        [JsonPropertyName("limit")]
        public int Limit { get; init; } = 100;

        [Range(0, 6000)]
        [JsonPropertyName("page")]
        public int Page { get; init; } = 1;

        [Range(0, 10000)]
        [JsonPropertyName("offset")]
        public int Offset { get; init; } = 0;

        [JsonPropertyName("sort")]
        public SortOrder SortBy { get; init; } = SortOrder.Ascending;
    }
}
