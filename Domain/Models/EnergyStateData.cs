using EnergyHeatMap.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Domain.Models
{
    public record EnergyStateData : IEnergyStateData
    {
        public EnergyStateData(string isoCode, string country, string energyStateValueType, string unit, IDateTimeWithValue[] values)
        {
            ISOCode = isoCode;
            Country = country;  
            EnergyStateValueType = energyStateValueType;
            Values = values;
            Unit = unit;
            Longname = $"{Country} {EnergyStateValueType} ({Unit})";
        }

        public string ISOCode { get; }
        public string Country { get; }
        public string EnergyStateValueType { get; } 
        public string Unit { get; }
        public IDateTimeWithValue[]? Values { get; }

        public string Longname { get; }
    }
}
