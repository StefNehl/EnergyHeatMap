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
    public class GetCorrelationCoefficentForHashrateAndValueHandler : IRequestHandler<GetCorrelationCoefficentForHashrateAndValueQuery, double>
    {
        private readonly IAnalysisService _service;
        public GetCorrelationCoefficentForHashrateAndValueHandler(IAnalysisService service)
        {
            _service = service;
        }
        public async Task<double> Handle(GetCorrelationCoefficentForHashrateAndValueQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCorrelationCoefficentForHashrateAndValue();
        }
    }
}
