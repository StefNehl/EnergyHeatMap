using EnergyHeatMap.Contracts.Repositories;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Services
{
    public class DataStatisticsService : IDataStatisticsService
    {
        public async Task<double> GetCorrelationCoefficent(double[] firstSet, double[] secondSet)
        {
            if (firstSet.Length != secondSet.Length)
                throw new ArgumentException("values must be the same length");

            var avg1 = firstSet.Average();
            var avg2 = secondSet.Average();

            var sum1 = firstSet.Zip(secondSet, (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

            var sumSqr1 = firstSet.Sum(x => Math.Pow((x - avg1), 2.0));
            var sumSqr2 = secondSet.Sum(y => Math.Pow((y - avg2), 2.0));

            var result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);

            return result;
        }

        public async Task<double[]> GetPolynomiamRegression(double[] firstSet, double[] secondSet, int order)
        {
            return Fit.Polynomial(firstSet, secondSet, order);
        }

    }
}
