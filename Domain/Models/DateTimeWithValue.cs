
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class DateTimeWithValue : IDateTimeWithValue
    {
        public DateTimeWithValue(DateTime dateTime, double value)
        {
            DateTime = dateTime;
            Value = value;
        }
        public DateTime DateTime { get; }
        public double Value { get; }
    }
}
