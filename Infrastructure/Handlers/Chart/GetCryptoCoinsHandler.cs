using EnergyHeatMap.Infrastructure.Queries.Chart;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers.Chart
{
    internal class GetCryptoCoinsHandler : IRequestHandler<GetCryptoCoinsQuery, IEnumerable<string>>
    {
        private readonly ICryptoCoinStateService _cryptoCoinStateService;

        public GetCryptoCoinsHandler(ICryptoCoinStateService service)
        {
            _cryptoCoinStateService = service;
        }

        public async Task<IEnumerable<string>> Handle(GetCryptoCoinsQuery request, CancellationToken cancellationToken)
        {
            return await _cryptoCoinStateService.GetCryptoCoins(cancellationToken);
        }
    }
}
