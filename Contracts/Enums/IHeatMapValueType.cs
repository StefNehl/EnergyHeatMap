using EnergyHeatMap.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Enums
{
    public interface IHeatMapValueType
    {
        string Name { get; }
        HeatMapValueTypes Type { get; }
    }
}
