using EnergyHeatMap.Contracts.Repositories;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Services
{
    public class DataPreparationService : IDataPreparationService
    {
        public double[] Extrapolate(double[] points, double[] value, double[] pointForInterpolation)
        {
            throw new NotImplementedException();
        }

        public Tuple<double, double>[] Interpolate(double[] points, double[] value, Tuple<double, double>[] arrayForInterpolation)
        {
            var interPolate = MathNet.Numerics.Interpolate.RationalWithoutPoles(points, value);

            for(int i = 0; i < arrayForInterpolation.Length; i++)
            {
                var pointValueTuple = arrayForInterpolation[i];
                if(arrayForInterpolation[i].Item2 == 0)
                    arrayForInterpolation[i] = new Tuple<double, double>(
                        pointValueTuple.Item1, 
                        interPolate.Interpolate(pointValueTuple.Item1));
            }

            return arrayForInterpolation;
        }
    }
}
