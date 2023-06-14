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
    public class MeasurementsController : ControllerBase
    {
        private readonly ILogger<MeasurementsController> _logger;
        private readonly IOpenAQApiWrapper _openAQApiWrapper;

        public MeasurementsController(ILogger<MeasurementsController> logger,
            IOpenAQApiWrapper openAQApiWrapper)
        {
            _logger = logger;
            _openAQApiWrapper = openAQApiWrapper;
        }

        [HttpGet("latest")]
        public async Task<PagedResult<LocationMeasurements>> Latest([FromQuery] LatestMeasurementsFilter measurementsFilter)
        {
            var response = await _openAQApiWrapper.GetLatestMeasurements(measurementsFilter);
            return PagedResult<LocationMeasurements>.FromOpenApiResponse(response);
        }
    }
}
