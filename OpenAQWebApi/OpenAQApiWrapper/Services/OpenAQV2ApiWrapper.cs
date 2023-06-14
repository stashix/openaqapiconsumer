using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using OpenAQApiWrapper.Entities;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Responses;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OpenAQApiWrapper.Services
{
    internal class OpenAQV2ApiWrapper : IOpenAQApiWrapper
    {
        private readonly ILogger<OpenAQV2ApiWrapper> _logger;
        private readonly HttpClient _httpClient;

        public OpenAQV2ApiWrapper(ILogger<OpenAQV2ApiWrapper> logger, HttpClient httpClient) 
        { 
            _logger = logger 
                ?? throw new ArgumentNullException(nameof(logger));

            _httpClient = httpClient 
                ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Result<PagedResponse<City>>> GetCitiesAsync(CitiesFilter citiesFilter, 
            CancellationToken cancellationToken = default)
        {
            if (citiesFilter is null)
                throw new ArgumentNullException(nameof(citiesFilter));

            var url = ConstructQuery("cities", citiesFilter);
            var result = await GetPagedAsync<City>(url, cancellationToken);

            return result;
        }

        public async Task<Result<PagedResponse<Country>>> GetCountriesAsync(CountriesFilter countriesFilter, 
            CancellationToken cancellationToken = default)
        {
            if (countriesFilter is null)
                throw new ArgumentNullException(nameof(countriesFilter));

            var url = ConstructQuery("countries", countriesFilter);
            var result = await GetPagedAsync<Country>(url, cancellationToken);

            return result;
        }

        public async Task<Result<PagedResponse<Country>>> GetCountryAsync(string countryCode, 
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentException("Cannot be null or whitespace.", nameof(countryCode));

            var result = await GetPagedAsync<Country>($"countries/{countryCode}", cancellationToken);

            return result;
        }

        public async Task<Result<PagedResponse<LocationMeasurements>>> GetLatestMeasurementsAsync(LatestMeasurementsFilter measurementsFilter, 
            CancellationToken cancellationToken = default)
        {
            if (measurementsFilter is null)
                throw new ArgumentNullException(nameof(measurementsFilter));

            var url = ConstructQuery("latest", measurementsFilter);
            var result = await GetPagedAsync<LocationMeasurements>(url, cancellationToken);

            return result;
        }

        private async Task<Result<PagedResponse<T>>> GetPagedAsync<T>(string relativeUrl, CancellationToken cancellationToken) where T : class
        {
            var correlationId = Guid.NewGuid();
            _logger.LogInformation("OpenAQ request {id} to {url}", correlationId, relativeUrl);

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, relativeUrl);

            try
            {
                using var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if(response.Content is null)
                    {
                        _logger.LogError("OpenAQ request {id} received a null response", correlationId);
                        return Result.Failure<PagedResponse<T>>("Received a null response");
                    }

                    var result = await response.Content.ReadFromJsonAsync<PagedResponse<T>>(cancellationToken: cancellationToken);
                    if(result is null)
                    {
                        _logger.LogError("OpenAQ request {id} could not deserialize content", correlationId);
                        return Result.Failure<PagedResponse<T>>("Could not deserialize content");
                    }

                    return Result.Success(result);
                }

                if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
                {
                    var validationFailedResponse = await response.Content.ReadFromJsonAsync<UnprocessableResponse>(cancellationToken: cancellationToken);
                    if(validationFailedResponse is null)
                    {
                        _logger.LogError("OpenAQ request {id} received a 422 error with no payload.", correlationId);
                        return Result.Failure<PagedResponse<T>>($"Received a {HttpStatusCode.UnprocessableEntity} status code.");
                    }

                    _logger.LogError("OpenAQ request {id} received a 422 error with {payload}", correlationId, validationFailedResponse);

                    var errorMessages = validationFailedResponse.Errors.Select(x => $"{string.Join(":", x.Location)} - {x.Message}");
                    return Result.Failure<PagedResponse<T>>(string.Join(",", errorMessages));
                }

                _logger.LogError("OpenAQ request {id} received {statusCode}", correlationId, response.StatusCode);

                return Result.Failure<PagedResponse<T>>($"Received a {response.StatusCode} status code.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "OpenAQ request {id} encountered {exceptionType}", correlationId, exception.GetType().Name);
                return Result.Failure<PagedResponse<T>>($"{exception.GetType().Name} when calling the API");
            }
        }

        private static string ConstructQuery<T>(string relativeUri, T filterModel) where T : class
        {
            var props = filterModel.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead);

            var keyValuePairsList = new List<KeyValuePair<string, StringValues>>();
            foreach(var prop in props)
            {
                var jsonPropertyName = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
                if (string.IsNullOrWhiteSpace(jsonPropertyName))
                    continue;

                var propertyValue = prop.GetValue(filterModel);
                if (propertyValue is null)
                    continue;

                if (prop.PropertyType.IsEnum || Nullable.GetUnderlyingType(prop.PropertyType)?.IsEnum == true)
                {
                    var enumType = prop.PropertyType.IsEnum ? prop.PropertyType 
                        : Nullable.GetUnderlyingType(prop.PropertyType);

                    var enumLabel = enumType?.GetTypeInfo()
                        .DeclaredMembers
                        .SingleOrDefault(x => x.Name == propertyValue.ToString())
                        ?.GetCustomAttribute<EnumMemberAttribute>()?.Value;

                    keyValuePairsList.Add(KeyValuePair.Create(jsonPropertyName, new StringValues(enumLabel)));
                    continue;
                }

                if (prop.PropertyType.IsAssignableFrom(typeof(IEnumerable<string>)))
                {
                    var collectionValue = propertyValue as IEnumerable<string>;
                    keyValuePairsList.Add(KeyValuePair.Create(jsonPropertyName, new StringValues(collectionValue?.ToArray())));
                    continue;
                }

                keyValuePairsList.Add(KeyValuePair.Create(jsonPropertyName, new StringValues(propertyValue.ToString())));
            };

            return QueryHelpers.AddQueryString(relativeUri, keyValuePairsList);
        }
    }
}
