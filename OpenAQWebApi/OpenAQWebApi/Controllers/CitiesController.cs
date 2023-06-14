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
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public CitiesController(ILogger<CitiesController> logger, 
            IOpenAQApiWrapper openAQApiWrapper)
        {
            _logger = logger;
            _openAQApiWrapper = openAQApiWrapper;
        }

        [HttpGet]
        public async Task<PagedResult<City>> Get([FromQuery] CitiesFilter citiesFilter)
        {
            var response = await _openAQApiWrapper.GetCities(citiesFilter);
            return PagedResult<City>.FromOpenApiResponse(response);
        }
    }
}
