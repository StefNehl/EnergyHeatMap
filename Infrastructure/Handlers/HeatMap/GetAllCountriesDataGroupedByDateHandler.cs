using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries.HeatMap;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers.HeatMap
{
    public class GetAllCountriesDataGroupedByDateHandler : IRequestHandler<GetAllCountriesDataGroupedByDateQuery, IDictionary<DateTime, IEnumerable<ICountryDataModel>>>
    {
        private readonly IHeatMapService _service;
        public GetAllCountriesDataGroupedByDateHandler(IHeatMapService service)
        {
            _service = service;
        }

        public async Task<IDictionary<DateTime, IEnumerable<ICountryDataModel>>> Handle(GetAllCountriesDataGroupedByDateQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCountriesDataGroupedByDateTime();
        }
    }
}
