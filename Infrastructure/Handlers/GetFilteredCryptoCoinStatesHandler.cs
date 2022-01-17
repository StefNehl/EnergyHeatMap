using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class GetFilteredCryptoCoinStatesHandler : IRequestHandler<GetFilteredCryptoCoinStatesQuery, IEnumerable<ICryptoCoinState>>
    {
        private readonly ICryptoCoinStateService _repo;
        public GetFilteredCryptoCoinStatesHandler(ICryptoCoinStateService repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ICryptoCoinState>> Handle(GetFilteredCryptoCoinStatesQuery request, 
            CancellationToken cancellationToken)
        {
            return _repo
                .GetCryptoCoinStateByFilter(
                request.Coinnames,
                request.Startdate,
                request.Enddate,
                default);
        }
    }
}
