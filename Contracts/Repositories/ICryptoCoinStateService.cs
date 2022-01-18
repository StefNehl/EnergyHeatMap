using EnergyHeatMap.Contracts.Models;

namespace EnergyHeatMap.Infrastructure.Services
{
    public interface ICryptoCoinStateService
    {
        Task<IEnumerable<ICryptoCoinState>> GetAllAsync(CancellationToken ct);

        Task<IEnumerable<ICryptoCoinState>> GetCryptoCoinStateByFilter(
            string[] coinname, 
            DateTime startdate, 
            DateTime enddate,
            CancellationToken ct);

        Task<IEnumerable<ICryptoStateData>> GetCryptoCoinDataFilteredByType(
            string[] coinname,
            string[] types,
            DateTime startdate,
            DateTime enddate,
            CancellationToken ct);

        Task<IEnumerable<string>> GetCryptoCoins(CancellationToken ct);
    }
}