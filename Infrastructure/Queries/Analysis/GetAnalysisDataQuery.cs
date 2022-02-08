using EnergyHeatMap.Contracts.Enums;
using MediatR;

namespace EnergyHeatMap.Infrastructure.Queries.Analysis
{
    public class GetAnalysisDataQuery : IRequest<IEnumerable<Tuple<double, double>>>
    {
        public GetAnalysisDataQuery(DateTime startDate, DateTime endDate, IAnalysisType analysisType)
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
