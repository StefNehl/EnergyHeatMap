using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IDataPreparationService
    {
        public Tuple<double, double>[] Interpolate(double[] points, double[] value, Tuple<double, double>[] arrayForInterpolation);

        public double[] Extrapolate(double[] points, double[] values, double[] pointForInterpolation);
    }
}
