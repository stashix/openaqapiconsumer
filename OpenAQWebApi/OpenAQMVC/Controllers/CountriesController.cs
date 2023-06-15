using Microsoft.AspNetCore.Mvc;
using OpenAQApiWrapper.Entities;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Services;
using OpenAQMVC.Models;

namespace OpenAQMVC.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public CountriesController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        public async Task<IActionResult> Index(CountriesFilterModel countriesFilter)
        {
            if (!ModelState.IsValid)
            {
                var vm = new PagedResultViewModel<CountriesFilterModel, Country>(countriesFilter, Array.Empty<Country>());
                return View(vm);
            }

            var countriesResult = await _openAQApiWrapper.GetCountriesAsync(new CountriesFilter()
            {
                Limit = countriesFilter.PageSize,
                Page = countriesFilter.Page,
                SortBy = countriesFilter.SortBy,
                OrderBy = countriesFilter.OrderBy,
                Countries = !string.IsNullOrWhiteSpace(countriesFilter.CountryId) 
                    ? new string[] { countriesFilter.CountryId }
                    : null,
            });

            if (countriesResult.IsFailure)
            {
                return View("Error", new ErrorViewModel()
                {
                    ErrorMessage = countriesResult.Error,
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            var viewModel = new PagedResultViewModel<CountriesFilterModel, Country>(countriesFilter, countriesResult.Value.Results);
            return View(viewModel);
        }
    }
}
