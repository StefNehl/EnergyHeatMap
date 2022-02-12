
using EnergyHeatMap.Contracts.Entities;

namespace EnergyHeatMap.Domain.Entities
{
    public class CountryEnergyStateEntity : ICountryEnergyStateEntity 
    {
        public CountryEnergyStateEntity(string isoCode, string country, DateTime dateTime, double population, 
            double primaryEnergyCons, string primaryEnergyConsUnit, double electricityGen, string electricityGenUnit)
        {
            IsoCode = isoCode;
            Country = country;
            DateTime = dateTime;    
            Population = population;
            PrimaryEnergyConsuption = primaryEnergyCons;
            PrimaryEnergyConsuptionUnit = primaryEnergyConsUnit;
            ElectricityGeneration = electricityGen;
            ElectricityGenerationUnit = electricityGenUnit;
        }

        public string IsoCode { get; }
        public string Country { get; }
        public DateTime DateTime { get; }
        public double Population { get; }

        //Needed setter for settings the interpolation Values
        public double PrimaryEnergyConsuption { get; set; }
        public string PrimaryEnergyConsuptionUnit { get; }
        public double ElectricityGeneration { get; }
        public string ElectricityGenerationUnit { get; }
    }
}
