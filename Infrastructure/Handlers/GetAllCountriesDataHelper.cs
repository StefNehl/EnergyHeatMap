using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class GetAllCountriesDataHelper : IRequestHandler<GetAllCountriesDataQuery, IEnumerable<ICountryDataModel>>
    {
        private readonly ICountryEnergyStateServices _service;

        public GetAllCountriesDataHelper(ICountryEnergyStateServices service)
        {
            _service = service;
        }

        public async Task<IEnumerable<ICountryDataModel>> Handle(GetAllCountriesDataQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCountriesData();
        }
    }
}
