namespace EnergyHeatMap.Contracts.Models
{
    public interface IDateTimeWithValue
    {
        DateTime DateTime { get; }
        double Value { get; }
    }
}