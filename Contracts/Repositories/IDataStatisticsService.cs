using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IDataStatisticsService
    {
        public Task<double> GetCorrelationCoefficent(double[] firstSet, double[] secondSet);
    }
}
