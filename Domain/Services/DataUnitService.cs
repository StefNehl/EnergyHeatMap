using EnergyHeatMap.Contracts.Repositories;

namespace EnergyHeatMap.Domain.Services
{
    public class DataUnitService : IDataUnitService
    {
        private readonly double _hashrateConvRate;
        private const string _unitHashrate = "EH/s";

        private readonly double _energyConvRate;
        private const string _unitEnergy = "TWh";

        private readonly double _valueConvRate;
        private const string _unitValue = "TUSD";

        public DataUnitService()
        {
            _hashrateConvRate = 1;
            _energyConvRate = 1;

            //_hashrateConvRate = Math.Pow(10, 18);
            _valueConvRate = Math.Pow(10, 3);
        }

        public double HashrateConvRate { get => _hashrateConvRate; }
        public string UnitHashrate { get => _unitHashrate; }

        public double EnergyConvRate { get => _energyConvRate; }
        public string UnitEnergy { get => _unitEnergy; }

        public double ValueConvRate { get => _valueConvRate; }
        public string UnitValue { get => _unitValue; }

        public double ConvertHashrate(double value)
        {
            return value / _hashrateConvRate;
        }
        public IEnumerable<double> ConvertHashrates(IEnumerable<double> values)
        {
            return values.Select(i => ConvertHashrate(i));
        }

        public double ConvertEnergy(double value)
        {
            return value / _energyConvRate;
        }
        public IEnumerable<double> ConvertEnergies(IEnumerable<double> values)
        {
            return values.Select(i => ConvertEnergy(i));
        }

        public double ConvertValue(double value)
        {
            return value / _valueConvRate;
        }
        public IEnumerable<double> ConvertValues(IEnumerable<double> values)
        {
            return values.Select(i => ConvertValue(i));
        }
    }
}
