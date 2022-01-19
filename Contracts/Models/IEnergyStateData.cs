using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Models
{
    public interface IEnergyStateData
    {
        string ISOCode { get; set; }
        string Country { get; set; }
        string EnergyStateValueTypes { get; set; }
        IDateTimeWithValue[]? Values {get;set;}
    }
}
