using Microsoft.AspNetCore.Mvc;
using OpenAQWebApi.Entities;
using OpenAQWebApi.Filters;
using OpenAQWebApi.Services;

namespace OpenAQWebApi.Controllers
{
    [ApiController]
    [Route("countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public CountriesController(ILogger<CountriesController> logger,
            IOpenAQApiWrapper openAQApiWrapper)
        {
            _logger = logger;
            _openAQApiWrapper = openAQApiWrapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> Countries()
        {
            var response = await _openAQApiWrapper.GetCountries(new CountriesFilter()
            {

            });

            return response.Results;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> Country()
        {
            var response = await _openAQApiWrapper.GetCountry(new CountryFilter()
            {

            });

            return response.Results;
        }
    }
}
