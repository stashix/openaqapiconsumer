using Microsoft.AspNetCore.Mvc;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Services;

namespace OpenAQMVC.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public CitiesController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        public async Task<IActionResult> Index()
        {
            var citiesResult = await _openAQApiWrapper.GetCitiesAsync(new CitiesFilter()
            {
                Limit = 50
            });

            return View(citiesResult.Value);
        }
    }
}
