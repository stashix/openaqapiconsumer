using Microsoft.AspNetCore.Mvc;
using OpenAQApiWrapper.Entities;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Services;
using OpenAQMVC.Models;

namespace OpenAQMVC.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public CitiesController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        public async Task<IActionResult> Index(CitiesFilterModel citiesFilter)
        {
            if (!ModelState.IsValid)
            {
                var vm = new PagedResultViewModel<CitiesFilterModel, City>(citiesFilter, Array.Empty<City>());
                return View(vm);
            }

            var citiesResult = await _openAQApiWrapper.GetCitiesAsync(new CitiesFilter()
            {
                Limit = citiesFilter.PageSize,
                Page = citiesFilter.Page,
                SortBy = citiesFilter.SortBy,
                OrderBy = citiesFilter.OrderBy,
                Countries = !string.IsNullOrWhiteSpace(citiesFilter.CountryId)
                    ? new string[] { citiesFilter.CountryId }
                    : null,
                Cities = !string.IsNullOrWhiteSpace(citiesFilter.CityName)
                    ? new string[] { citiesFilter.CityName }
                    : null
            });

            if (citiesResult.IsFailure)
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrorMessage = citiesResult.Error,
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            var viewModel = new PagedResultViewModel<CitiesFilterModel, City>(citiesFilter, citiesResult.Value.Results);
            return View(viewModel);
        }
    }
}
