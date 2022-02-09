using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries.Chart;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers.Chart
{
    public class GetEnergyStateValueTypesHandler : IRequestHandler<GetEnergyStateValueTypesQuery, IEnumerable<IEnergyStateValueType>>
    {
        private readonly ICountryEnergyStateServices _countryEnergyStateServices;
        public GetEnergyStateValueTypesHandler(ICountryEnergyStateServices servicve)
        {
            _countryEnergyStateServices = servicve;
        }
        public async Task<IEnumerable<IEnergyStateValueType>> Handle(GetEnergyStateValueTypesQuery request, CancellationToken cancellationToken)
        {
            return await _countryEnergyStateServices.GetEnergyStateValueTypes(cancellationToken);
        }
    }
}
