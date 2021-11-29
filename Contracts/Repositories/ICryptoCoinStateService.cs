using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Infrastructure.Services
{
    public interface ICryptoCoinStateService
    {
        Task<IEnumerable<ICryptoCoinState>> GetAllAsync(CancellationToken ct);
    }
}