namespace EnergyHeatMap.Contracts.Models
{
    public interface IDateTimeWithValue
    {
        DateTime DateTime { get; set; }
        double Value { get; set; }
    }
}