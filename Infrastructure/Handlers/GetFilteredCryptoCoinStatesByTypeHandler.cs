using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class GetFilteredCryptoCoinStatesByTypeHandler : IRequestHandler<GetFilteredCryptoCoinStatesByTypeQuery, IEnumerable<ICryptoStateData>>
    {
        private readonly ICryptoCoinStateService _repo;
        public GetFilteredCryptoCoinStatesByTypeHandler(ICryptoCoinStateService repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ICryptoStateData>> Handle(GetFilteredCryptoCoinStatesByTypeQuery request, 
            CancellationToken cancellationToken)
        {
            return await _repo
                .GetCryptoCoinDataFilteredByType(
                request.Coinnames,
                request.Types,
                request.Startdate,
                request.Enddate,
                default);
        }
    }
}
