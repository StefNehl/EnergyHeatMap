using EnergyHeatMap.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Services
{
    public class DataPreparationService : IDataPreparationService
    {
        public IEnumerable<Tuple<DateTime, double>> Extrapolate(IEnumerable<Tuple<DateTime, double>> data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<DateTime, double>> Interpolate(IEnumerable<Tuple<DateTime, double>> data)
        {
            throw new NotImplementedException();
        }
    }
}
