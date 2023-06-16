using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System;
using Xunit;
using OpenAQApiWrapperTest.Mocks;
using OpenAQApiWrapper.Services;
using Microsoft.Extensions.Logging;
using Moq;
using OpenAQApiWrapper.Responses;
using OpenAQApiWrapper.Entities;
using System.Collections.Generic;
using OpenAQApiWrapper.Filters;
using System.Threading.Tasks;

namespace OpenAQApiWrapperTest
{
    public class MeasurementsTests
    {
        [Fact]
        public async Task WhenOkLatestMeasurementsRequest_ThenReturnsOkResult()
        {
            var responseBody = new PagedResponse<LocationMeasurements>
            {
                Metadata = new Metadata()
                {
                    Limit = 5,
                    Found = 5,
                    License = "",
                    Page = 1,
                    Name = "OpenAQ",
                    WebSite = ""
                },
                Results = new List<LocationMeasurements>()
                {

                }
            };

            var apiResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseBody))
            };

            var apiWrapper = GetWrapper(apiResponse);
            var result = await apiWrapper.GetLatestMeasurementsAsync(new LatestMeasurementsFilter()
            {
                Limit = 5,
                SortBy = SortOrder.Ascending,
                Page = 1
            });

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task WhenOKLatestMeasurementsRequest_NoPayload_ThenReturnsFailResult()
        {
            var apiResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(null))
            };

            var apiWrapper = GetWrapper(apiResponse);
            var result = await apiWrapper.GetLatestMeasurementsAsync(new LatestMeasurementsFilter()
            {
                Limit = 5,
                SortBy = SortOrder.Ascending,
                Page = 1
            });

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task WhenOKLatestMeasurementsRequest_UnserializablePayload_ThenReturnsFailResult()
        {
            var responseBody = new
            {
                Prop1 = "something",
                Prop2 = "something else"
            };

            var apiResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseBody))
            };

            var apiWrapper = GetWrapper(apiResponse);
            var result = await apiWrapper.GetLatestMeasurementsAsync(new LatestMeasurementsFilter()
            {
                Limit = 5,
                SortBy = SortOrder.Ascending,
                Page = 1
            });

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task WhenBadRequestLatestMeasurementsRequest_ThenReturnsFailResult()
        {
            var responseBody = new UnprocessableResponse()
            {
                Errors = new List<ValidationError>()
                {
                    new ValidationError()
                    {
                        Location = new List<string>{ "part1", "part2" },
                        Message = "Some malformed data",
                        Type = "datatype"
                    }
                }
            };

            var apiResponse = new HttpResponseMessage(HttpStatusCode.UnprocessableEntity)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseBody))
            };

            var apiWrapper = GetWrapper(apiResponse);
            var result = await apiWrapper.GetLatestMeasurementsAsync(new LatestMeasurementsFilter()
            {
                Limit = 5,
                SortBy = SortOrder.Ascending,
                Page = 1
            });

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task WhenInternalServerErrorLatestMeasurementsRequest_ThenReturnsFailResult()
        {
            var responseBody = "Internal Server Error";

            var apiResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(JsonConvert.SerializeObject(responseBody))
            };

            var apiWrapper = GetWrapper(apiResponse);
            var result = await apiWrapper.GetLatestMeasurementsAsync(new LatestMeasurementsFilter()
            {
                Limit = 5,
                SortBy = SortOrder.Ascending,
                Page = 1
            });

            Assert.True(result.IsFailure);
        }

        private static OpenAQV2ApiWrapper GetWrapper(HttpResponseMessage apiResponse)
        {
            var logger = new Mock<ILogger<OpenAQV2ApiWrapper>>();
            var querySerializer = new ReflectionQuerySerializer();
            var httpClient = new HttpClient(new MockedHttpMessageHandler(apiResponse))
            {
                BaseAddress = new Uri("http://localhost")
            };

            var apiWrapper = new OpenAQV2ApiWrapper(logger.Object, querySerializer, httpClient);

            return apiWrapper;
        }
    }
}