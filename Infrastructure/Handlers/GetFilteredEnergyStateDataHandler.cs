using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class GetFilteredEnergyStateDataHandler : IRequestHandler<GetFilteredEnergyStateDataQuery, IEnumerable<IEnergyStateData>>
    {
        private readonly ICountryEnergyStateServices _repo;

        public GetFilteredEnergyStateDataHandler(ICountryEnergyStateServices service)
        {
            _repo = service;
        }
        public async Task<IEnumerable<IEnergyStateData>> Handle(GetFilteredEnergyStateDataQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetEnergyStateDataByType(
                request.Countries,
                request.Types,
                request.Startdate,
                request.Enddate,
                cancellationToken);
        }
    }
}
