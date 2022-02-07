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
    public class GetCorrelationCoefficentHandler : IRequestHandler<GetCorrelationCoefficentQuery, double>
    {
        private readonly IDataStatisticsService _service;
        public GetCorrelationCoefficentHandler(IDataStatisticsService service)
        {
            _service = service;
        }
        public async Task<double> Handle(GetCorrelationCoefficentQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCorrelationCoefficent(request.Set1, request.Set2);
        }
    }
}
