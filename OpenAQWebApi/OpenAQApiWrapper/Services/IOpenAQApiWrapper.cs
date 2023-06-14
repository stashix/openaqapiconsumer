using CSharpFunctionalExtensions;
using OpenAQApiWrapper.Entities;
using OpenAQApiWrapper.Filters;
using OpenAQApiWrapper.Responses;

namespace OpenAQApiWrapper.Services
{
    public interface IOpenAQApiWrapper
    {
        Task<Result<PagedResponse<City>>> GetCitiesAsync(CitiesFilter citiesFilter,
            CancellationToken cancellationToken = default);

        Task<Result<PagedResponse<Country>>> GetCountryAsync(string countryCode, 
            CancellationToken cancellationToken = default);

        Task<Result<PagedResponse<Country>>> GetCountriesAsync(CountriesFilter countryFilter, 
            CancellationToken cancellationToken = default);

        Task<Result<PagedResponse<LocationMeasurements>>> GetLatestMeasurementsAsync(LatestMeasurementsFilter measurementsFilter, 
            CancellationToken cancellationToken = default);
    }
}
