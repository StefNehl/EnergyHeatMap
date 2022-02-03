using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries.HeatMap;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Handlers.HeatMap
{
    public class GetHeatMapValueTypesHandler : IRequestHandler<GetHeatMapValueTypesQuery, IEnumerable<IHeatMapValueType>>
    {
        private readonly IHeatMapService _service;
        public GetHeatMapValueTypesHandler(IHeatMapService service)
        {
            _service = service; 
        }

        public async Task<IEnumerable<IHeatMapValueType>> Handle(GetHeatMapValueTypesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetHeatMapValueTypes();
        }
    }
}
