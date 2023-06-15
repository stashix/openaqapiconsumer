using OpenAQApiWrapper.Entities;

namespace OpenAQMVC.Models
{
    public interface IPagingFilterModel
    {
        int Page { get; }

        int PageSize { get; }

        SortOrder SortBy { get; }

        IPagingFilterModel MoveToPage(int page);
    }
}
