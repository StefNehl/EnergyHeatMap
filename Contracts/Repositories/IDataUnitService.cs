using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IDataUnitService
    {
        double EnergyConvRate { get; }
        double HashrateConvRate { get; }
        string UnitEnergy { get; }
        string UnitHashrate { get; }
        string UnitValue { get; }
        double ValueConvRate { get; }

        IEnumerable<double> ConvertEnergies(IEnumerable<double> values);
        double ConvertEnergy(double value);
        double ConvertHashrate(double value);
        IEnumerable<double> ConvertHashrates(IEnumerable<double> values);
        double ConvertValue(double value);
        IEnumerable<double> ConvertValues(IEnumerable<double> values);
    }
}
