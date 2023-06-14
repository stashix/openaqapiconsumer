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
    public class CountriesController : ControllerBase
    {
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public CountriesController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Country>>> Get([FromQuery] CountriesFilter countriesFilter)
        {
            var result = await _openAQApiWrapper.GetCountriesAsync(countriesFilter);

            if (result.IsFailure)
                return StatusCode(500, new ErrorResult(result.Error));

            return new PagedResult<Country>(result.Value);
        }

        [HttpGet("{code:regex([[A-Za-z]][[A-Za-z]])}")]
        public async Task<ActionResult<Country>> Get(string code)
        {
            var result = await _openAQApiWrapper.GetCountryAsync(code);

            if (result.IsFailure)
                return StatusCode(500, new ErrorResult(result.Error));

            if (result.Value.Results.Count == 0)
                return NotFound();

            return result.Value.Results.First();
        }
    }
}
