using Microsoft.AspNetCore.Mvc;
using OpenAQApiWrapper.Entities;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Services;
using OpenAQWebApi.Results;
using System.Net;

namespace OpenAQWebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MeasurementsController : ControllerBase
    {
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public MeasurementsController(IOpenAQApiWrapper openAQApiWrapper)
        {
            _openAQApiWrapper = openAQApiWrapper;
        }

        [HttpGet("latest")]
        public async Task<ActionResult<PagedResult<LocationMeasurements>>> Get([FromQuery] LatestMeasurementsFilter measurementsFilter)
        {
            var result = await _openAQApiWrapper.GetLatestMeasurementsAsync(measurementsFilter);

            if (result.IsFailure)
                return StatusCode(500, new ErrorResult(result.Error));

            return new PagedResult<LocationMeasurements>(result.Value);
        }
    }
}
