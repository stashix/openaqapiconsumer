using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using OpenAQWebApi.Entities;
using OpenAQWebApi.Filters;
using OpenAQWebApi.Responses;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OpenAQWebApi.Services
{
    public class OpenAQV2ApiWrapper : IOpenAQApiWrapper
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

        public async Task<PagedResponse<City>> GetCities(CitiesFilter citiesFilter)
        {
            if (citiesFilter is null)
                throw new ArgumentNullException(nameof(citiesFilter));

            var url = CreateQuery("cities", citiesFilter);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url)) 
            {
                var response = await _httpClient.SendAsync(requestMessage);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadFromJsonAsync<PagedResponse<City>>();

                return result;
            }
        }

        public async Task<PagedResponse<Country>> GetCountries(CountriesFilter countriesFilter)
        {
            if (countriesFilter is null)
                throw new ArgumentNullException(nameof(countriesFilter));

            var url = CreateQuery("countries", countriesFilter);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await _httpClient.SendAsync(requestMessage);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadFromJsonAsync<PagedResponse<Country>>();

                return result;
            }
        }

        public async Task<PagedResponse<Country>> GetCountry(CountryFilter countryFilter)
        {
            if (countryFilter is null)
                throw new ArgumentNullException(nameof(countryFilter));

            var url = CreateQuery("countries", countryFilter);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await _httpClient.SendAsync(requestMessage);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadFromJsonAsync<PagedResponse<Country>>();

                return result;
            }
        }

        public async Task<PagedResponse<LocationMeasurements>> GetLatestMeasurements(LatestMeasurementsFilter measurementsFilter)
        {
            if (measurementsFilter is null)
                throw new ArgumentNullException(nameof(measurementsFilter));

            var url = CreateQuery("latest", measurementsFilter);

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await _httpClient.SendAsync(requestMessage);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadFromJsonAsync<PagedResponse<LocationMeasurements>>();

                return result;
            }
        }

        private static string CreateQuery<T>(string relativeUri, T filterModel) where T : class
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
