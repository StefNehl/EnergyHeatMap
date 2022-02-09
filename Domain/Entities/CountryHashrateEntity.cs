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
        public CountryHashrateEntity(DateTime dateTime, string country, double monthlyHashratePerc, double monthlyHashrateAbs, string monthlyHashrateAbsUnit)
        {
            DateTime = dateTime;
            Country = country;  
            MonthlyHashratePercentage = monthlyHashratePerc;
            MonthlyHashrateAbsolut  = monthlyHashrateAbs;
            MonthlyHashrateUnit = monthlyHashrateAbsUnit;
        }

        public DateTime DateTime { get; }
        public string Country { get; }
        public double MonthlyHashratePercentage { get; }
        public double MonthlyHashrateAbsolut { get; }
        public string MonthlyHashrateUnit { get; }
    }
}
