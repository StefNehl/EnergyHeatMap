using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Contracts.Entities
{
    public interface ICryptoCoinStateEntity
    {
        string CoinName { get; set; }
        DateTime DateTime { get; set; }
        double Difficulty { get; set; }
        double Hashrate { get; set; }
        double Value { get; set; }
    }
}