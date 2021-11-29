using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class CryptoCoinState : ICryptoCoinState
    {
        public DateTime DateTime { get; set; }
        public string CoinName { get; set; } = Contracts.Enums.CoinName.None;
        public decimal Value { get; set; }
        public decimal Hashrate { get; set; }
        public decimal Difficulty { get; set; }
    }
}
