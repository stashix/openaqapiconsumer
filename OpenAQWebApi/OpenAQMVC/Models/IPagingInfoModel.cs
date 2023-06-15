namespace OpenAQMVC.Models
{
    public interface IPagingInfoModel<out TFilter> where TFilter : IPagingFilterModel
    {
        TFilter CurrentFilter { get; }

        bool HasNext { get; }

        bool HasPrevious { get; }

        IPagingFilterModel Next();

        IPagingFilterModel Previous();
    }
}
