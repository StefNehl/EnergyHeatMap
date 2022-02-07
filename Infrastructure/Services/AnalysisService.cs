using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using EnergyHeatMap.Infrastructure.Queries.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IMediator _mediator;
        public AnalysisService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<double> GetCorrelationCoefficentForHashrateAndValue()
        {
            var coinValueQuery = new GetAllCryptoCoinStatesQuery();
            var coinState = await _mediator.Send(coinValueQuery);

            var values = coinState.Select(x => x.Value).ToArray();
            var hashRate = coinState.Select(y => y.Hashrate).ToArray();

            var corrCoefQuery = new GetCorrelationCoefficentQuery(values, hashRate);
            return await _mediator.Send(corrCoefQuery);
        }
    }
}
