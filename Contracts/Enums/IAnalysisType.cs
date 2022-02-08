using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Enums
{
    public interface IAnalysisType
    {
        string Name { get; }
        AnalysisTypes Type { get; }
    }
}
