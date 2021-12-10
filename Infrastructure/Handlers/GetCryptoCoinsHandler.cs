using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
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
