using OpenAQApiWrapper.Entities;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Responses;

namespace OpenAQApiWrapper.Services
{
    public interface IOpenAQApiWrapper
    {
        Task<PagedResponse<City>> GetCities(CitiesFilter citiesFilter);

        Task<PagedResponse<Country>> GetCountry(CountryFilter countryFilter);

        Task<PagedResponse<Country>> GetCountries(CountriesFilter countryFilter);

        Task<PagedResponse<LocationMeasurements>> GetLatestMeasurements(LatestMeasurementsFilter measurementsFilter);
    }
}
