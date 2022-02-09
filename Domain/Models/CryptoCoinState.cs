using EnergyHeatMap.Contracts.Enums;
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class CryptoCoinState : ICryptoCoinState
    {
        public CryptoCoinState(DateTime dateTime, string coinName, double value, string valueUnit, double hashrate, string hashrateUnit)
        {
            DateTime = dateTime;
            CoinName = coinName;
            Value = value;
            ValueUnit = valueUnit;
            Hashrate = hashrate;
            HashrateUnit = hashrateUnit;
        }
        public DateTime DateTime { get; }
        public string CoinName { get; }
        public double Value { get; }
        public string ValueUnit { get; }
        public double Hashrate { get; }
        public string HashrateUnit { get; }
    }
}
