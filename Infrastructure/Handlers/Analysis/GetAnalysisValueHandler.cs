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
    public class GetAnalysisValueHandler : IRequestHandler<GetAnalysisValueQuery, double>
    {
        private readonly IAnalysisService _service;
        public GetAnalysisValueHandler(IAnalysisService service)
        {
            _service = service;
        }
        public async Task<double> Handle(GetAnalysisValueQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAnalysisValue(request.StartDate, request.EndDate, request.AnalysisType);
        }
    }
}
