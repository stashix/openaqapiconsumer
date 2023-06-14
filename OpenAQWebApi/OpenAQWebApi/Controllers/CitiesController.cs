using Microsoft.AspNetCore.Mvc;
using OpenAQWebApi.Entities;
using OpenAQWebApi.Filters;
using OpenAQWebApi.Services;

namespace OpenAQWebApi.Controllers
{
    [ApiController]
    [Route("cities")]
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

        [HttpGet(Name = "GetCities")]
        public async Task<IEnumerable<City>> Get()
        {
            var response = await _openAQApiWrapper.GetCities(new CitiesFilter()
            {
                Countries = new string[] { "CZ", "SK" }
            });

            return response.Results;
        }
    }
}
