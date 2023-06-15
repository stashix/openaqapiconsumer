using OpenAQApiWrapper.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpenAQMVC.Models
{
    public abstract class PagingFilterModel : IPagingFilterModel
    {
        public static readonly int[] PageSizes = { 10, 50, 100 };

        public PagingFilterModel() { }

        public PagingFilterModel(PagingFilterModel model, int page) 
        { 
            if(model is null)
                throw new ArgumentNullException(nameof(model));

            if(page < 0) 
                throw new ArgumentOutOfRangeException(nameof(page));

            Page = page;
            PageSize = model.PageSize;
            SortBy = model.SortBy;
        }

        [Range(1, 6000)]
        public int Page { get; set; } = 1;

        [Range(1, 10000)]
        public int PageSize { get; init; } = 50;

        [Required]
        public SortOrder SortBy { get; init; } = SortOrder.Ascending;

        public abstract IPagingFilterModel MoveToPage(int page);
    }
}
