using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers.Chart
{
    public class GetFilteredCryptoCoinStatesHandler : IRequestHandler<GetFilteredCryptoCoinStatesQuery, IEnumerable<ICryptoCoinState>>
    {
        private readonly ICryptoCoinStateService _repo;
        public GetFilteredCryptoCoinStatesHandler(ICryptoCoinStateService repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ICryptoCoinState>> Handle(GetFilteredCryptoCoinStatesQuery request, 
            CancellationToken cancellationToken)
        {
            return await _repo
                .GetCryptoCoinStateByFilter(
                request.Coinnames,
                request.Startdate,
                request.Enddate,
                default);
        }
    }
}
