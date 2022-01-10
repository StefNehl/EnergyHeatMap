using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Handlers
{
    public class GetCountriesHandler : IRequestHandler<GetCountriesQuery, IEnumerable<string>>
    {
        private readonly ICountryEnergyStateServices _countryEnergyStateServices;

        public GetCountriesHandler(ICountryEnergyStateServices service)
        {
            _countryEnergyStateServices = service;
        }

        public async Task<IEnumerable<string>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            return await _countryEnergyStateServices.GetCountries(cancellationToken);
        }
    }
}
