using OpenAQApiWrapper.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpenAQMVC.Models
{
    public class CountriesFilterModel : PagingFilterModel
    {
        public CountriesFilterModel() : base() { }

        public CountriesFilterModel(CountriesFilterModel model, int page) : base(model, page)
        {
            CountryId = model.CountryId;
            OrderBy = model.OrderBy;
        }

        [RegularExpression("[A-Za-z][A-Za-z]")]
        public string? CountryId { get; init; }

        public CountryOrder? OrderBy { get; init; }

        public override CountriesFilterModel MoveToPage(int page) => new(this, page);
    }
}
