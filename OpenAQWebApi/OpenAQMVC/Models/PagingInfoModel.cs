namespace OpenAQMVC.Models
{
    public class PagingInfoModel<TFilter> : IPagingInfoModel<TFilter> 
        where TFilter : IPagingFilterModel
    {
        public PagingInfoModel(TFilter currentFilter, long resultsCount)
        {
            if(currentFilter is null)
                throw new ArgumentNullException(nameof(currentFilter));

            if(resultsCount < 0) 
                throw new ArgumentOutOfRangeException(nameof(resultsCount));

            CurrentFilter = currentFilter;
            //OpenAQ does not return the total number of results on many endpoints, so there is no way to know
            //if we're on the last page until we overflow it when the total number is divisible by pagesize.
            //This also means we can't have links to specific page numbers.
            HasNext = resultsCount > 0 && resultsCount == currentFilter.PageSize;
            HasPrevious = currentFilter.Page > 1;
        }

        public TFilter CurrentFilter { get; }

        public bool HasNext { get; }

        public bool HasPrevious { get; }

        public IPagingFilterModel Next()
        {
            if (HasNext)
            {
                return CurrentFilter.MoveToPage(CurrentFilter.Page + 1);
            }
            
            return CurrentFilter;
        }

        public IPagingFilterModel Previous()
        {
            if (HasPrevious)
            {
                return CurrentFilter.MoveToPage(CurrentFilter.Page - 1);
            }

            return CurrentFilter;
        }
    }
}
