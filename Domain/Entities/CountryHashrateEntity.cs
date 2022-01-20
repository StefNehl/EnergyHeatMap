using EnergyHeatMap.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Entities
{
    public class CountryHashrateEntity : ICountryHashrateEntity
    {
        public DateTime DateTime { get; set; }
        public string Country { get; set; } = string.Empty;
        public double MonthlyHashratePercentage { get; set; }
        public double MonthlyHashrateAbsolut { get; set; }
    }
}
