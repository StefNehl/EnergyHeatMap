using EnergyHeatMap.Contracts.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyHeatMap.Infrastructure.Queries.Analysis
{
    public class GetAnalysisValueQuery : IRequest<double>
    {
        public GetAnalysisValueQuery(DateTime startDate, DateTime endDate, IAnalysisType analysisType)
        {
            StartDate = startDate;
            EndDate = endDate;
            AnalysisType = analysisType;
        }

        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public IAnalysisType AnalysisType { get; }
    }
}
