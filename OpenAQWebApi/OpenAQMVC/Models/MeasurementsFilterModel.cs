using OpenAQApiWrapper.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpenAQMVC.Models
{
    public class MeasurementsFilterModel : PagingFilterModel
    {
        public MeasurementsFilterModel() : base() { }

        public MeasurementsFilterModel(MeasurementsFilterModel model, int page) : base(model, page)
        {
            CountryId = model.CountryId;
            CityName = model.CityName;
            OrderBy = model.OrderBy;
        }

        [RegularExpression("[A-Za-z][A-Za-z]")]
        public string? CountryId { get; init; }

        public string? CityName { get; init; }

        public MeasurementsOrder? OrderBy { get; init; }

        public override MeasurementsFilterModel MoveToPage(int page) => new(this, page);
    }
}
