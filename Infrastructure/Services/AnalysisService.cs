using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;
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

        private async Task<IEnumerable<ICryptoCoinState>> GetHashrateAndValueData(DateTime startDate, DateTime endDate)
        {
            var coinValueQuery = new GetAllCryptoCoinStatesQuery();
            return (await _mediator.Send(coinValueQuery)).Where(i => i.DateTime >= startDate && i.DateTime <= endDate);
        }

        private async Task<double> GetCorrelationCoefficentForHashrateAndValue(DateTime startDate, DateTime endDate)
        {
            var coinState = await GetHashrateAndValueData(startDate, endDate);

            var values = coinState.Select(x => x.Value).ToArray();
            var hashRate = coinState.Select(y => y.Hashrate).ToArray();

            var corrCoefQuery = new GetCorrelationCoefficentQuery(values, hashRate);
            return await _mediator.Send(corrCoefQuery);
        }

        public async Task<IEnumerable<Tuple<double, double>>> GetAnalysisDataSet(DateTime startDate, DateTime endDate, IAnalysisType analysisType)
        {
            var typeEnum = analysisType.Type;
            var result = new List<Tuple<double, double>>();


            switch(analysisType.Type)
            {
                case AnalysisTypes.CorrelationHashrateValue:
                    var data = await GetHashrateAndValueData(startDate, endDate);
                    result.AddRange(data.Select(i => new Tuple<double, double>(i.Hashrate, i.Value)));
                    break;
                default:
                    break;
            }

            return result;
        }

        public async Task<double> GetAnalysisValue(DateTime startDate, DateTime endDate, IAnalysisType analysisType)
        {
            switch (analysisType.Type)
            {
                case AnalysisTypes.CorrelationHashrateValue:
                    return await GetCorrelationCoefficentForHashrateAndValue(startDate, endDate);
                default:
                    return 0;
            }
        }

        public async Task<IEnumerable<IAnalysisType>> GetAnalysisTypes()
        {
            return AnalysisTypesExtansion.GetValues();
        }
    }
}
