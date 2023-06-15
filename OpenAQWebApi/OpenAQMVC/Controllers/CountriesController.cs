using Microsoft.AspNetCore.Mvc;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Services;

namespace OpenAQMVC.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public CountriesController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        public async Task<IActionResult> Index()
        {
            var countriesResult = await _openAQApiWrapper.GetCountriesAsync(new CountriesFilter()
            {
                Limit = 50
            });

            return View(countriesResult.Value);
        }
    }
}
