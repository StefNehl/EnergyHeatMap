using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Contracts.Models
{
    public interface ICryptoCoinState
    {
        string CoinName { get; }
        DateTime DateTime { get; }
        double Hashrate { get; }
        string HashrateUnit { get; }
        double Value { get; }
        string ValueUnit { get; }
    }
}