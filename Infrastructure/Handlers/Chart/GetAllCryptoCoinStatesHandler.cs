using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers.Chart
{
    public class GetAllCryptoCoinStatesHandler : IRequestHandler<GetAllCryptoCoinStatesQuery, IEnumerable<ICryptoCoinState>>
    {
        private readonly ICryptoCoinStateService _cryptoCoinStateService;

        public GetAllCryptoCoinStatesHandler(ICryptoCoinStateService service)
        {
            _cryptoCoinStateService = service;
        }

        public async Task<IEnumerable<ICryptoCoinState>> Handle(GetAllCryptoCoinStatesQuery request, CancellationToken cancellationToken)
        {
            return await _cryptoCoinStateService.GetAllAsync(cancellationToken);
        }
    }
}
