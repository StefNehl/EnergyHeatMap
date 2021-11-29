using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Domain.Entities
{
    public class CryptoCoinStateEntity : ICryptoCoinStateEntity
    {
        public DateTime DateTime { get; set; }
        public CoinName CoinName { get; set; }
        public decimal Value { get; set; }
        public decimal Hashrate { get; set; }
        public decimal Difficulty { get; set; }
    }
}
