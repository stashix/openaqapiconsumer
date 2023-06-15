using OpenAQApiWrapper.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpenAQMVC.Models
{
    public class CitiesFilterModel : PagingFilterModel
    {
        public CitiesFilterModel() : base() { }

        public CitiesFilterModel(CitiesFilterModel model, int page) : base(model, page)
        {
            CountryId = model.CountryId;
            CityName = model.CityName;
            OrderBy = model.OrderBy;
        }

        [RegularExpression("[A-Za-z][A-Za-z]")]
        public string? CountryId { get; init; }

        public string? CityName { get; init; }

        public CityOrder? OrderBy { get; init; }

        public override CitiesFilterModel MoveToPage(int page) => new(this, page);
    }
}
