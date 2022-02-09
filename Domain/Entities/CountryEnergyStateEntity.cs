
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
        public double PrimaryEnergyConsuption { get; }
        public string PrimaryEnergyConsuptionUnit { get; }
        public double ElectricityGeneration { get; }
        public string ElectricityGenerationUnit { get; }


        //Maybe not needed
        public double Coal_prod_change_pct { get; set; }
        public double Coal_prod_change_twh { get; set; }
        public double Gas_prod_change_pct { get; set; }
        public double Gas_prod_change_twh { get; set; }
        public double Oil_prod_change_pct { get; set; }
        public double Oil_prod_change_twh { get; set; }
        public double Energy_cons_change_pct { get; set; }
        public double Energy_cons_change_twh { get; set; }
        public double Biofuel_share_elec { get; set; }
        public double Biofuel_elec_per_capita { get; set; }
        public double Biofuel_cons_change_pct { get; set; }
        public double Biofuel_share_energy { get; set; }
        public double Biofuel_cons_change_twh { get; set; }
        public double Biofuel_consumption { get; set; }
        public double Biofuel_cons_per_capita { get; set; }
        public double Carbon_intensity_elec { get; set; }
        public double Coal_share_elec { get; set; }
        public double Coal_cons_change_pct { get; set; }
        public double Coal_share_energy { get; set; }
        public double Coal_cons_change_twh { get; set; }
        public double Coal_consumption { get; set; }
        public double Coal_elec_per_capita { get; set; }
        public double Coal_cons_per_capita { get; set; }
        public double Ccoal_production { get; set; }
        public double Coal_prod_per_capita { get; set; }
        public double Biofuel_electricity { get; set; }
        public double Coal_electricity { get; set; }
        public double Fossil_electricity { get; set; }
        public double Gas_electricity { get; set; }
        public double Hydro_electricity { get; set; }
        public double Nuclear_electricity { get; set; }
        public double Oil_electricity { get; set; }
        public double Other_renewable_electricity { get; set; }
        public double Other_renewable_exc_biofuel_electricity { get; set; }
        public double Renewables_electricity { get; set; }
        public double Solar_electricity { get; set; }
        public double Wind_electricity { get; set; }
        public double Energy_per_gdp { get; set; }
        public double Energy_per_capita { get; set; }
        public double Fossil_cons_change_pct { get; set; }
        public double Fossil_share_energy { get; set; }
        public double Fossil_cons_change_twh { get; set; }
        public double Fossil_fuel_consumption { get; set; }
        public double Fossil_energy_per_capita { get; set; }
        public double Fossil_cons_per_capita { get; set; }
        public double Fossil_share_elec { get; set; }
        public double Gas_share_elec { get; set; }
        public double Gas_cons_change_pct { get; set; }
        public double Gas_share_energy { get; set; }
        public double Gas_cons_change_twh { get; set; }
        public double Gas_consumption { get; set; }
        public double Gas_elec_per_capita { get; set; }
        public double Gas_energy_per_capita { get; set; }
        public double Gas_production { get; set; }
        public double Gas_prod_per_capita { get; set; }


    }
}
