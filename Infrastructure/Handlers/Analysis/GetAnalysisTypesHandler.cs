using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure.Queries.Analysis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Handlers.Analysis
{
    public class GetAnalysisTypesHandler : IRequestHandler<GetAnalysisTypesQuery, IEnumerable<IAnalysisType>>
    {
        private readonly IAnalysisService _service;
        public GetAnalysisTypesHandler(IAnalysisService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<IAnalysisType>> Handle(GetAnalysisTypesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAnalysisTypes();
        }
    }
}
