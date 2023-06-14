using CSharpFunctionalExtensions;
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
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public CitiesController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<City>>> Get([FromQuery] CitiesFilter citiesFilter)
        {
            var result = await _openAQApiWrapper.GetCitiesAsync(citiesFilter);

            if (result.IsFailure)
                return StatusCode(500, new ErrorResult(result.Error));

            return new PagedResult<City>(result.Value);
        }
    }
}
