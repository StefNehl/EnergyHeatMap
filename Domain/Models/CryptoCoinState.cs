using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class CryptoCoinState : ICryptoCoinState
    {
        public DateTime DateTime { get; set; }
        public string CoinName { get; set; } = Contracts.Enums.CoinName.None;
        public double Value { get; set; }
        public double Hashrate { get; set; }
        public double Difficulty { get; set; }
    }
}
