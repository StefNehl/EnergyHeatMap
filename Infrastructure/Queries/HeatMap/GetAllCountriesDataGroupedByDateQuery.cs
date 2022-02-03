using EnergyHeatMap.Contracts.Models;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Queries.HeatMap
{
    public class GetAllCountriesDataGroupedByDateQuery : IRequest<IDictionary<DateTime, IEnumerable<ICountryDataModel>>>
    {
    }
}
