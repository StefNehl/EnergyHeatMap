using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Handlers
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
