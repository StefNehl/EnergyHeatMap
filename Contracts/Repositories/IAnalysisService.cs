using EnergyHeatMap.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IAnalysisService
    {
        Task<IEnumerable<IAnalysisType>> GetAnalysisTypes();
        Task<IEnumerable<Tuple<double, double>>> GetAnalysisDataSet(DateTime startDate, DateTime endDate, IAnalysisType analysisType);
        Task<double> GetAnalysisValue(DateTime startDate, DateTime endDate, IAnalysisType analysisType);
    }
}
