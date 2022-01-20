using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Models
{
    public interface IEnergyStateData
    {
        string ISOCode { get;}
        string Country { get;}
        string EnergyStateValueType { get;}
        string Unit { get;}
        IDateTimeWithValue[]? Values {get;}
    }
}
