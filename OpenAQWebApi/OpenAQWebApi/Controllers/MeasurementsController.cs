using Microsoft.AspNetCore.Mvc;
using OpenAQWebApi.Entities;
using OpenAQWebApi.Filters;
using OpenAQWebApi.Services;

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
        public async Task<IEnumerable<LocationMeasurements>> Latest([FromQuery] LatestMeasurementsFilter measurementsFilter)
        {
            var response = await _openAQApiWrapper.GetLatestMeasurements(measurementsFilter);
            return response.Results;
        }
    }
}
