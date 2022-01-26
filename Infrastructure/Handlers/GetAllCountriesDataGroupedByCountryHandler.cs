using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class GetAllCountriesDataGroupedByCountryHandler : IRequestHandler<GetAllCountriesDataGroupedByCountryQuery, IDictionary<string, IEnumerable<ICountryDataModel>>>
    {
        private readonly ICountryEnergyStateServices _service;
        public GetAllCountriesDataGroupedByCountryHandler(ICountryEnergyStateServices service)
        {
            _service = service;
        }

        public async Task<IDictionary<string, IEnumerable<ICountryDataModel>>> Handle(GetAllCountriesDataGroupedByCountryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCountriesDataGroupedByCountry();
        }
    }
}
