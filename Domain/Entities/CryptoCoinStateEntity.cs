using EnergyHeatMap.Contracts.Entities;
using EnergyHeatMap.Contracts.Enums;

namespace EnergyHeatMap.Domain.Entities
{
    public class CryptoCoinStateEntity : ICryptoCoinStateEntity
    {
        public CryptoCoinStateEntity(DateTime dateTime, string coinName, double value, string valueUnit)
        {
            DateTime = dateTime;
            CoinName = coinName;
            Value = value;
            ValueUnit = valueUnit;
        }

        public DateTime DateTime { get; }
        public string CoinName { get; }
        public double Value { get; }
        public string ValueUnit { get; }
        public double Hashrate { get; set; }
        public string HashrateUnit { get; set; }

    }
}
