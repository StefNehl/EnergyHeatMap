namespace EnergyHeatMap.Contracts.Entities
{
    public interface ICountryEnergyStateEntity
    {
        string IsoCode { get; }
        string Country { get; }
        DateTime DateTime { get; }
        double Population { get; }
        double PrimaryEnergyConsuption { get; }
        string PrimaryEnergyConsuptionUnit { get; }
        double ElectricityGeneration { get; }
        string ElectricityGenerationUnit { get; }
    }
}