
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class CryptoStateData : ICryptoStateData
    {
        public string CoinName { get; set; } = string.Empty;
        public string ValueType { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public IDateTimeWithValue[]? Values { get; set; }
    }
}
