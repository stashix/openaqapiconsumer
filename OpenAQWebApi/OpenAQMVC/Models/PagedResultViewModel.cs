namespace OpenAQMVC.Models
{
    public class PagedResultViewModel<TFilter, TResultItem> 
        where TFilter : PagingFilterModel
        where TResultItem : class
    {
        public PagedResultViewModel(TFilter currentFilter, IList<TResultItem> results) 
        { 
            Results = results
                ?? throw new ArgumentNullException(nameof(results));

            PagingInfo = new PagingInfoModel<TFilter>(currentFilter, results.Count);
        }

        public IPagingInfoModel<TFilter> PagingInfo { get; }

        public IList<TResultItem> Results { get; }
    }
}
