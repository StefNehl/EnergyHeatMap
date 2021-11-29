using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Contracts.Models
{
    public interface ICryptoCoinState
    {
        CoinName CoinName { get; set; }
        DateTime DateTime { get; set; }
        decimal Difficulty { get; set; }
        decimal Hashrate { get; set; }
        decimal Value { get; set; }
    }
}