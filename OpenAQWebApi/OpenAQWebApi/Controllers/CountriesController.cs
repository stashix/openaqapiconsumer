using Microsoft.AspNetCore.Mvc;
using OpenAQApiWrapper.Entities;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Services;
using OpenAQWebApi.Results;

namespace OpenAQWebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
        public async Task<PagedResult<Country>> Get([FromQuery] CountriesFilter countriesFilter)
        {
            var response = await _openAQApiWrapper.GetCountries(countriesFilter);
            return PagedResult<Country>.FromOpenApiResponse(response);
        }

        [HttpGet("{code}")]
        public async Task<IEnumerable<Country>> Get(string code)
        {
            var response = await _openAQApiWrapper.GetCountry(new CountryFilter()
            {
                CountryId = code
            });

            return response.Results;
        }
    }
}
