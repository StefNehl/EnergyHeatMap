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
    public class GetPolynomiamRegressionHandler : IRequestHandler<GetPolynomiamRegressionQuery, double[]>
    {
        private readonly IDataStatisticsService _service;
        public GetPolynomiamRegressionHandler(IDataStatisticsService service)
        {
            _service = service;
        }

        public async Task<double[]> Handle(GetPolynomiamRegressionQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPolynomiamRegression(request.FirstSet, request.SecondSet, request.Order);
        }
    }
}
