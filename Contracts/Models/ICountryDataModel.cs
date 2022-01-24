using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Models
{
    public interface ICountryDataModel
    {
        string CountryName { get; }
        DateTime DateTime { get; }  
        double HashrateAbs { get; }
        string HashrateAbsUnit { get; }
        double HashratePerc { get; }
        double EnergyConsumptionAbs { get; }
        string EnergyConsumptionAbsUnit { get; }
        double EnergyConsumptionPerc { get; }
    }
}
