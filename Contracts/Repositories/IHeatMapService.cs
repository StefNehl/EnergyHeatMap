using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IHeatMapService
    {
        Task<Dictionary<DateTime, IEnumerable<ICountryDataModel>>> GetCountriesDataGroupedByDateTime();
        Task<IEnumerable<IHeatMapValueType>> GetHeatMapValueTypes();
    }
}
