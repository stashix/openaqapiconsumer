using OpenAQApiWrapper.Responses;
using System.Text.Json.Serialization;

namespace OpenAQWebApi.Results
{
    public class PagedResult<T> where T : class
    {
        public PagedResult() 
        { 
            Results = Enumerable.Empty<T>();
        }

        [JsonPropertyName("pageNumber")]
        public int Page { get; init; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; init; }

        [JsonPropertyName("mayHaveMore")]
        public bool MayHaveMore { get; init; }

        [JsonPropertyName("resultCount")]
        public int ResultCount { get; init; }

        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; init; }

        public static PagedResult<T> FromOpenApiResponse(PagedResponse<T> pagedResponse)
        {
            return new PagedResult<T>()
            {
                MayHaveMore = pagedResponse is not null 
                    && pagedResponse.Metadata.Found >= pagedResponse.Metadata.Limit,
                Page = pagedResponse?.Metadata?.Page ?? 0,
                PageSize = pagedResponse?.Metadata?.Limit ?? 0,
                ResultCount = pagedResponse?.Metadata?.Found ?? 0,
                Results = pagedResponse?.Results ?? Enumerable.Empty<T>()
            };
        }
    }
}
