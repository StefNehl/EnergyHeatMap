using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries.Domain
{
    public class GetCorrelationCoefficentQuery : IRequest<double>
    {
        public GetCorrelationCoefficentQuery(double[] set1, double[] set2)
        {
            Set1 = set1;
            Set2 = set2;
        }

        public double[] Set1 { get; }
        public double[] Set2 { get; }
           
    }
}
