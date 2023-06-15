using Microsoft.AspNetCore.Mvc;
using OpenAQApiWrapper.Entities;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Services;
using OpenAQMVC.Models;

namespace OpenAQMVC.Controllers
{
    public class MeasurementsController : Controller
    {
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public MeasurementsController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        public async Task<IActionResult> Index(MeasurementsFilterModel measurementsFilter)
        {
            if (!ModelState.IsValid)
            {
                var vm = new PagedResultViewModel<MeasurementsFilterModel, LocationMeasurements>(measurementsFilter, 
                        Array.Empty<LocationMeasurements>());

                return View(vm);
            }

            var measurementsResult = await _openAQApiWrapper.GetLatestMeasurementsAsync(new LatestMeasurementsFilter()
            {
                Limit = measurementsFilter.PageSize,
                Page = measurementsFilter.Page,
                SortBy = measurementsFilter.SortBy,
                OrderBy = measurementsFilter.OrderBy,
                Countries = !string.IsNullOrWhiteSpace(measurementsFilter.CountryId)
                    ? new string[] { measurementsFilter.CountryId }
                    : null,
                Cities = !string.IsNullOrWhiteSpace(measurementsFilter.CityName)
                    ? new string[] { measurementsFilter.CityName }
                    : null
            });

            if (measurementsResult.IsFailure)
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrorMessage = measurementsResult.Error,
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            var viewModel = new PagedResultViewModel<MeasurementsFilterModel, LocationMeasurements>(measurementsFilter, 
                measurementsResult.Value.Results);

            return View(viewModel);
        }
    }
}
