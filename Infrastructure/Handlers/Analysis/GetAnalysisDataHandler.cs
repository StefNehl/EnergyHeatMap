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
    internal class GetAnalysisDataHandler : IRequestHandler<GetAnalysisDataQuery, IEnumerable<Tuple<double, double>>>
    {
        private readonly IAnalysisService _service;
        public GetAnalysisDataHandler(IAnalysisService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<Tuple<double, double>>> Handle(GetAnalysisDataQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAnalysisDataSet(request.StartDate, request.EndDate, request.AnalysisType);
        }
    }
}
