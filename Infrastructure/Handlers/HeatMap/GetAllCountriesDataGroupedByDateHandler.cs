using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
