using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Contracts.Entities
{
    public interface ICryptoCoinStateEntity
    {
        string CoinName { get; }
        DateTime DateTime { get; }
        double Hashrate { get; set; }
        string HashrateUnit { get; set; }
        double Value { get; }
        string ValueUnit { get; }
    }
}