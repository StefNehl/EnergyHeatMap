using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Entities
{
    public interface ICountryHashrateEntity
    {
        DateTime DateTime { get; set; }
        string Country { get; set; }    
        double MonthlyHashratePercentage { get; set; }
        double MonthlyHashrateAbsolut { get; set; }
        
    }
}
