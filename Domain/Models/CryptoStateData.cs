
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public record CryptoStateData : ICryptoStateData
    {
        public CryptoStateData(string coinName, string valueType, string unit, IDateTimeWithValue[] values)
        {
            CoinName = coinName;
            ValueType = valueType;
            Unit = unit;
            Values = values;
            Longname = $"{CoinName} {ValueType} ({Unit})";
        }

        public string CoinName { get; }
        public string ValueType { get; }
        public string Unit { get; }
        public IDateTimeWithValue[]? Values { get; }
        public string Longname { get; }
    }
}
