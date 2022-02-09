using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class CountryEnergyStateModel : ICountryEnergyStateModel
    {
        public CountryEnergyStateModel(string isoCode, string country, DateTime year, double population, 
            double primaryEnergyCons, string primaryEnergyConsUnit, double electricityGeneration, string electricityGenerationUnit)
        {
            IsoCode = isoCode;
            Country = country;
            Year = year;
            Population = population;
            PrimaryEnergyConsuption = primaryEnergyCons;
            PrimaryEnergyConsuptionUnit = primaryEnergyConsUnit;
            ElectricityGeneration = electricityGeneration;
            ElectricityGenerationUnit = electricityGenerationUnit;
        }

        public string IsoCode { get; }
        public string Country { get; }
        public DateTime Year { get; }
        public double Population { get; }
        public double PrimaryEnergyConsuption { get; }
        public string PrimaryEnergyConsuptionUnit { get; }
        public double ElectricityGeneration { get; }
        public string ElectricityGenerationUnit { get; }
    }
}
