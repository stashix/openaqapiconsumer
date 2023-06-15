using OpenAQApiWrapper.Filters;

namespace OpenAQApiWrapper.Services
{
    public interface IQuerySerializer
    {
        string ConstructQuery<T>(string relativeUri, T filterModel) where T : PagingFilter;
    }
}
