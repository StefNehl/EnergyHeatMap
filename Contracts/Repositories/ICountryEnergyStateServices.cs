using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface ICountryEnergyStateServices
    {
        Task<IEnumerable<string>> GetCountries(CancellationToken ct);

        Task<IEnumerable<IEnergyStateData>> GetEnergyStateDataByType(string[] countries,
           string[] types,
           DateTime startdate = default,
           DateTime enddate = default,
           CancellationToken ct = default);

        Task<IEnumerable<ICountryDataModel>> GetCountriesData(CancellationToken ct);

        Task<Dictionary<string, IEnumerable<ICountryDataModel>>> GetCountriesDataGroupedByCountry(CancellationToken ct);

        Task<IEnumerable<IEnergyStateValueType>> GetEnergyStateValueTypes(CancellationToken ct);

        Task InitService(CancellationToken ct);
    }
}
