using OpenAQWebApi.Entities;
using OpenAQWebApi.Filters;
using OpenAQWebApi.Responses;

namespace OpenAQWebApi.Services
{
    public interface IOpenAQApiWrapper
    {
        Task<PagedResponse<City>> GetCities(CitiesFilter citiesFilter);

        Task<PagedResponse<Country>> GetCountry(CountryFilter countryFilter);

        Task<PagedResponse<Country>> GetCountries(CountriesFilter countryFilter);

        Task<PagedResponse<LocationMeasurements>> GetLatestMeasurements(LatestMeasurementsFilter measurementsFilter);
    }
}
