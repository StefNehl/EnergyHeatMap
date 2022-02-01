using EnergyHeatMap.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Models
{
    public class CountryDataModel : ICountryDataModel
    {
        public CountryDataModel(string countryName, string countryCode, DateTime dateTime, 
            double hashrateAbs, string hashrateAbsUnit, double hashratePerc, 
            double energyConsumptionAbs, string energyConsumptionAbsUnit, double energyConsumptionPerc)
        {
            CountryName = countryName;
            CountryCode = countryCode;
            DateTime = dateTime;
            HashrateAbs = hashrateAbs;
            HashrateAbsUnit = hashrateAbsUnit;
            HashratePerc = hashratePerc;
            EnergyConsumptionAbs = energyConsumptionAbs;
            EnergyConsumptionAbsUnit = energyConsumptionAbsUnit;
            EnergyConsumptionPerc = energyConsumptionPerc;
        }

        public string CountryName { get; }
        public string CountryCode { get; }
        public DateTime DateTime { get; }   
        public double HashrateAbs { get; }
        public string HashrateAbsUnit { get; }
        public double HashratePerc { get; }
        public double EnergyConsumptionAbs { get; }
        public string EnergyConsumptionAbsUnit { get; }
        public double EnergyConsumptionPerc { get; }

    }
}
