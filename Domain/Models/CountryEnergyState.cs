using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class CountryEnergyStateModel : ICountryEnergyStateModel
    {
        public string IsoCode { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        public decimal Population { get; set; }
        public decimal Primary_energy_consuption { get; set; }
        public decimal Electricity_generation { get; set; }
    }
}
