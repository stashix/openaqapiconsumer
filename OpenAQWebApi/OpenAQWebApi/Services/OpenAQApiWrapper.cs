using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using OpenAQWebApi.Entities;
using OpenAQWebApi.Filters;
using OpenAQWebApi.Responses;
using System.Reflection;
using System.Text.Json.Serialization;

namespace OpenAQWebApi.Services
{
    public class OpenAQApiWrapper : IOpenAQApiWrapper
    {
        private readonly ILogger<OpenAQApiWrapper> _logger;
        private readonly HttpClient _httpClient;

        public OpenAQApiWrapper(ILogger<OpenAQApiWrapper> logger, HttpClient httpClient) 
        { 
            _logger = logger 
                ?? throw new ArgumentNullException(nameof(logger));

            _httpClient = httpClient 
                ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<PagedResponse<City>?> GetCities(CitiesFilter citiesFilter)
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

            throw new NotImplementedException();
        }

        public async Task<PagedResponse<Country>> GetCountry(CountryFilter countryFilter)
        {
            if (countryFilter is null)
                throw new ArgumentNullException(nameof(countryFilter));

            throw new NotImplementedException();
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

                if (prop.PropertyType.IsEnum)
                {
                    var enumLabel = propertyValue.
                    continue;
                }

                if (prop.PropertyType.IsAssignableFrom(typeof(IEnumerable<string>)))
                {
                    var collectionValue = propertyValue as IEnumerable<string>;
                    keyValuePairsList.Add(KeyValuePair.Create(jsonPropertyName, new StringValues(collectionValue.ToArray())));
                    continue;
                }

                keyValuePairsList.Add(KeyValuePair.Create(jsonPropertyName, new StringValues(propertyValue.ToString())));
            };

            return QueryHelpers.AddQueryString(relativeUri, keyValuePairsList);
        }
    }
}
