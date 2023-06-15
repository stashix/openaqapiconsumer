using Microsoft.AspNetCore.Mvc;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Services;

namespace OpenAQMVC.Controllers
{
    public class MeasurementsController : Controller
    {
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public MeasurementsController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        public async Task<IActionResult> Index()
        {
            var measurementsResult = await _openAQApiWrapper.GetLatestMeasurementsAsync(new LatestMeasurementsFilter()
            {
                Limit = 50
            });

            return View(measurementsResult.Value);
        }
    }
}
