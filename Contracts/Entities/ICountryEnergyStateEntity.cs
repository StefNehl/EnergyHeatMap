namespace EnergyHeatMap.Contracts.Entities
{
    public interface ICountryEnergyStateEntity
    {
        string IsoCode { get; set; }
        string Country { get; set; }
        DateTime DateTime { get; set; }
        long Population { get; set; }
        decimal Primary_energy_consuption { get; set; }
        decimal Electricity_generation { get; set; }
    }
}