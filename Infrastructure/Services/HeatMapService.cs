using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Domain.Enums;
using EnergyHeatMap.Infrastructure.Queries;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Services
{


    public class HeatMapService : IHeatMapService
    {
        private readonly IMediator _mediator;

        public HeatMapService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Dictionary<DateTime, IEnumerable<ICountryDataModel>>> GetCountriesDataGroupedByDateTime()
        {
            var countryQuery = new GetAllCountriesDataQuery();

            var countriesData = await _mediator.Send(countryQuery);

            var groups = countriesData.GroupBy(i => i.DateTime);
            var result = new Dictionary<DateTime, IEnumerable<ICountryDataModel>>();

            foreach (var group in groups)
            {
                var countryGroupKey = group.Key;
                var values = group;
                result.Add(countryGroupKey, group.ToList());
            }

            return result;
        }

        public async Task<IEnumerable<IHeatMapValueType>> GetHeatMapValueTypes()
        {
            await Task.Yield();
            return HeatMapValueTypesExtensions.GetValues();
        }
    }
}
