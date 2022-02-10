using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries.Domain
{
    public class GetPolynomiamRegressionQuery : IRequest<double[]>
    {
        public GetPolynomiamRegressionQuery(double[] firstSet, double[] secondSet, int order)
        {
            FirstSet = firstSet;
            SecondSet = secondSet;
            Order = order;
        }

        public double[] FirstSet { get; set; }
        public double[] SecondSet { get; set; }
        public int Order { get; set; }
    }
}
