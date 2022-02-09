using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Handlers.Domain
{
    public class GetDataUnitServiceHandler : IRequestHandler<GetDataUnitServiceQuery, IDataUnitService>
    {
        private readonly IDataUnitService _dataUnitService;
        public GetDataUnitServiceHandler(IDataUnitService dataUnitService)
        {
            _dataUnitService = dataUnitService;
        }
        public async Task<IDataUnitService> Handle(GetDataUnitServiceQuery request, CancellationToken cancellationToken)
        {
            return _dataUnitService;
        }
    }
}
