using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Domain.Entities
{
    public class CryptoCoinStateEntity : ICryptoCoinStateEntity
    {
        public DateTime DateTime { get; set; }
        public string CoinName { get; set; } = Contracts.Enums.CoinName.None;
        public double Value { get; set; }
        public double Hashrate { get; set; }
        public double Difficulty { get; set; }
    }
}
