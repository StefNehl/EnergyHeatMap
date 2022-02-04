using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Handlers.Chart
{
    public class GetCryptoValueTypesHandler : IRequestHandler<GetCryptoValueTypesQuery, IEnumerable<ICryptoValueType>>
    {
        private readonly ICryptoCoinStateService _service;
        public GetCryptoValueTypesHandler(ICryptoCoinStateService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<ICryptoValueType>> Handle(GetCryptoValueTypesQuery request, CancellationToken cancellationToken)
        {
            return _service.GetCryptoCoinValueTypes();
        }
    }
}
