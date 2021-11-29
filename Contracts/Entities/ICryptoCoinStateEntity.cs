using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Contracts.Entities
{
    public interface ICryptoCoinStateEntity
    {
        string CoinName { get; set; }
        DateTime DateTime { get; set; }
        decimal Difficulty { get; set; }
        decimal Hashrate { get; set; }
        decimal Value { get; set; }
    }
}