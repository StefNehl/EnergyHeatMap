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
            var data = await _mediator.Send(coinValueQuery);

            return data.Where(i => i.DateTime >= startDate && i.DateTime <= endDate);
        }

        private async Task<double> GetCorrelationCoefficentForHashrateAndValue(DateTime startDate, DateTime endDate)
        {
            var coinState = await GetHashrateAndValueData(startDate, endDate);

            var values = coinState.Select(x => x.Value).ToArray();
            var hashRate = coinState.Select(y => y.Hashrate).ToArray();

            var corrCoefQuery = new GetCorrelationCoefficentQuery(values, hashRate);
            
            return await _mediator.Send(corrCoefQuery);
        }

        private async Task<IEnumerable<Tuple<double, double>>> GetEnergyAndHashrateData(DateTime startDate, DateTime endDate)
        {
            var result = new List<Tuple<double, double>>();
            var coinValueQuery = new GetAllCryptoCoinStatesQuery();
            var hashRateData = await _mediator.Send(coinValueQuery);

            var type = EnergyStateValueTypes.PrimaryEnergyConsumption;
            var energyQuery = new GetFilteredEnergyStateDataQuery(new string[1] { "World"}, new string[1] { type.ToString() }, startDate, endDate);
            var energyData = (await _mediator.Send(energyQuery)).FirstOrDefault() ;

            if (energyData == null)
                return result;

            foreach(var hashrate in hashRateData)
            {
                var energy = energyData.Values.FirstOrDefault(i => i.DateTime == hashrate.DateTime);
                if (energy == null)
                    continue;
                result.Add(new Tuple<double, double>(hashrate.Hashrate, energy.Value));
            }

            return result;
        }

        private async Task<double> GetCorrelationCoefficentForEnergyAndHashrate(DateTime startDate, DateTime endDate)
        {
            var data = await GetEnergyAndHashrateData(startDate, endDate);

            var hashrate = data.Select(x => x.Item1).ToArray();
            var energy = data.Select(y => y.Item2).ToArray();

            var corrCoefQuery = new GetCorrelationCoefficentQuery(hashrate, energy);

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
                case AnalysisTypes.CorrelationEnergyHashrate:
                    result.AddRange(await GetEnergyAndHashrateData(startDate, endDate));
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
                case AnalysisTypes.CorrelationEnergyHashrate:
                    return await GetCorrelationCoefficentForEnergyAndHashrate(startDate, endDate);
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
