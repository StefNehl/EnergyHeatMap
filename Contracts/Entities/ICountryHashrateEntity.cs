using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Entities
{
    public interface ICountryHashrateEntity
    {
        DateTime DateTime { get; }
        string Country { get; }    
        double MonthlyHashratePercentage { get; }
        double MonthlyHashrateAbsolut { get; }
        string MonthlyHashrateUnit { get; }
        
    }
}
