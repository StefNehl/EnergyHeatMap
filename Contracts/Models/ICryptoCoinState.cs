using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Contracts.Models
{
    public interface ICryptoCoinState
    {
        string CoinName { get; set; }
        DateTime DateTime { get; set; }
        double Difficulty { get; set; }
        double Hashrate { get; set; }
        double Value { get; set; }
    }
}