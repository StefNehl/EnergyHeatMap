
namespace EnergyHeatMap.Contracts.Repositories
{
    public interface IDataStatisticsService
    {
        Task<double> GetCorrelationCoefficent(double[] firstSet, double[] secondSet);

        Task<double[]> GetPolynomiamRegression(double[] firstSet, double[] secondSet, int order);
    }
}
