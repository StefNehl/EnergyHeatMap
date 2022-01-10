
using EnergyHeatMap.Contracts.Entities;

namespace EnergyHeatMap.Domain.Entities
{
    public class CountryEnergyStateEntity : ICountryEnergyStateEntity 
    {
        public string IsoCode { get; set; }
        public string Country { get; set; }
        public DateTime DateTime { get; set; }
        public long Population { get; set; }
        public decimal Primary_energy_consuption { get; set; }
        public decimal Electricity_generation { get; set; }


        //Maybe not needed
        public decimal Coal_prod_change_pct { get; set; }
        public decimal Coal_prod_change_twh { get; set; }
        public decimal Gas_prod_change_pct { get; set; }
        public decimal Gas_prod_change_twh { get; set; }
        public decimal Oil_prod_change_pct { get; set; }
        public decimal Oil_prod_change_twh { get; set; }
        public decimal Energy_cons_change_pct { get; set; }
        public decimal Energy_cons_change_twh { get; set; }
        public decimal Biofuel_share_elec { get; set; }
        public decimal Biofuel_elec_per_capita { get; set; }
        public decimal Biofuel_cons_change_pct { get; set; }
        public decimal Biofuel_share_energy { get; set; }
        public decimal Biofuel_cons_change_twh { get; set; }
        public decimal Biofuel_consumption { get; set; }
        public decimal Biofuel_cons_per_capita { get; set; }
        public decimal Carbon_intensity_elec { get; set; }
        public decimal Coal_share_elec { get; set; }
        public decimal Coal_cons_change_pct { get; set; }
        public decimal Coal_share_energy { get; set; }
        public decimal Coal_cons_change_twh { get; set; }
        public decimal Coal_consumption { get; set; }
        public decimal Coal_elec_per_capita { get; set; }
        public decimal Coal_cons_per_capita { get; set; }
        public decimal Ccoal_production { get; set; }
        public decimal Coal_prod_per_capita { get; set; }
        public decimal Biofuel_electricity { get; set; }
        public decimal Coal_electricity { get; set; }
        public decimal Fossil_electricity { get; set; }
        public decimal Gas_electricity { get; set; }
        public decimal Hydro_electricity { get; set; }
        public decimal Nuclear_electricity { get; set; }
        public decimal Oil_electricity { get; set; }
        public decimal Other_renewable_electricity { get; set; }
        public decimal Other_renewable_exc_biofuel_electricity { get; set; }
        public decimal Renewables_electricity { get; set; }
        public decimal Solar_electricity { get; set; }
        public decimal Wind_electricity { get; set; }
        public decimal Energy_per_gdp { get; set; }
        public decimal Energy_per_capita { get; set; }
        public decimal Fossil_cons_change_pct { get; set; }
        public decimal Fossil_share_energy { get; set; }
        public decimal Fossil_cons_change_twh { get; set; }
        public decimal Fossil_fuel_consumption { get; set; }
        public decimal Fossil_energy_per_capita { get; set; }
        public decimal Fossil_cons_per_capita { get; set; }
        public decimal Fossil_share_elec { get; set; }
        public decimal Gas_share_elec { get; set; }
        public decimal Gas_cons_change_pct { get; set; }
        public decimal Gas_share_energy { get; set; }
        public decimal Gas_cons_change_twh { get; set; }
        public decimal Gas_consumption { get; set; }
        public decimal Gas_elec_per_capita { get; set; }
        public decimal Gas_energy_per_capita { get; set; }
        public decimal Gas_production { get; set; }
        public decimal Gas_prod_per_capita { get; set; }
    }
}
