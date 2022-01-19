using EnergyHeatMap.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Models
{
    public class EnergyStateData : IEnergyStateData
    {
        public string ISOCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string EnergyStateValueTypes { get; set; } = string.Empty;
        public IDateTimeWithValue[]? Values { get; set; }
    }
}
