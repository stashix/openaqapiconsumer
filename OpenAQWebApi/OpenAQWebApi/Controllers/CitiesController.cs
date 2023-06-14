using Microsoft.AspNetCore.Mvc;
using OpenAQWebApi.Entities;
using OpenAQWebApi.Filters;
using OpenAQWebApi.Services;

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
        public async Task<IEnumerable<City>> Get([FromQuery] CitiesFilter citiesFilter)
        {
            var response = await _openAQApiWrapper.GetCities(citiesFilter);
            return response.Results;
        }
    }
}
