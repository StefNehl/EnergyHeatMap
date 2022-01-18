
using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Domain.Models
{
    public class DateTimeWithValue : IDateTimeWithValue
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
}
