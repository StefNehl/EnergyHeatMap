using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Models
{
    public interface ICountryEnergyStateModel
    {
        string IsoCode { get; }
        string Country { get; }
        DateTime Year { get; }
        double Population { get; }
        double PrimaryEnergyConsuption { get; }
        string PrimaryEnergyConsuptionUnit { get; }
        double ElectricityGeneration { get; }
        string ElectricityGenerationUnit { get; }
    }
}
