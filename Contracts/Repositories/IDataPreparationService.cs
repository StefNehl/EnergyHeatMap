using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IDataPreparationService
    {
        public IEnumerable<Tuple<DateTime, double>> Interpolate(IEnumerable<Tuple<DateTime, double>> data);

        public IEnumerable<Tuple<DateTime, double>> Extrapolate(IEnumerable<Tuple<DateTime, double>> data);
    }
}
